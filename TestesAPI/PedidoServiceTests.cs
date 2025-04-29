using APIdeTestes.Domain;
using APIdeTestes.Repositories;
using APIdeTestes.Services;
using AutoFixture;
using Bogus;
using FluentAssertions;
using Moq;
using Xunit;

namespace TestesAPI
{
    public class PedidoServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Faker _faker;
        private readonly Mock<IPedidoRepository> _pedidoRepoMock;
        private readonly Mock<IProdutoRepository> _produtoRepoMock;
        private readonly PedidoService _pedidoService;

        public PedidoServiceTests()
        {
            _fixture = new Fixture();
            _faker = new Faker("pt_BR");
            _pedidoRepoMock = new Mock<IPedidoRepository>();
            _produtoRepoMock = new Mock<IProdutoRepository>();
            _pedidoService = new PedidoService(_pedidoRepoMock.Object, _produtoRepoMock.Object);
        }

        [Theory]
        [InlineData(1, 2, 100.50, 10)]
        [InlineData(2, 3, 250.00, 20)]
        public void CriarPedido_Valido_DeveCriarComSucesso(int clienteId, int produtoId, double valor, int estoque)
        {
            // Arrange
            var cliente = new Cliente(clienteId, _faker.Name.FullName(), _faker.Internet.Email(), _faker.Phone.PhoneNumber());
            var produto = new Produto(produtoId, _faker.Commerce.ProductName(), (decimal)valor, estoque);
            var dataPedido = DateTime.Now;

            _produtoRepoMock.Setup(x => x.ObterPorId(produtoId)).Returns(produto);

            var pedido = new Pedido(0, clienteId, cliente, dataPedido);
            pedido.AdicionarItem(produto, 1); // Corrigido aqui

            // Act
            _pedidoService.CriarPedido(pedido);

            // Assert
            pedido.ClienteId.Should().Be(clienteId);
            pedido.Itens.Should().ContainSingle(i => i.Produto == produto); // Corrigido aqui
            pedido.DataPedido.Date.Should().Be(dataPedido.Date);

            _pedidoRepoMock.Verify(x => x.Adicionar(It.IsAny<Pedido>()), Times.Once);
        }


        [Theory]
        [InlineData(-1, 2, 50, 5)]
        [InlineData(1, 2, -100, 8)]
        public void CriarPedido_DadosInvalidos_DeveLancarExcecao(int clienteId, int produtoId, double valor, int estoque)
        {
            // Arrange
            var cliente = new Cliente(clienteId, _faker.Name.FullName(), _faker.Internet.Email(), _faker.Phone.PhoneNumber());
            var produto = new Produto(produtoId, _faker.Commerce.ProductName(), (decimal)valor, estoque);
            var dataPedido = DateTime.Now;

            _produtoRepoMock.Setup(x => x.ObterPorId(produtoId)).Returns(produto);

            var pedido = new Pedido(0, clienteId, cliente, dataPedido);
            pedido.AdicionarItem(produto, 1); // CORRETO


            // Act
            Action act = () => _pedidoService.CriarPedido(pedido);

            // Assert
            act.Should().Throw<ArgumentException>();

            _pedidoRepoMock.Verify(x => x.Adicionar(It.IsAny<Pedido>()), Times.Never);
        }

        [Fact]
        public void CriarPedido_ProdutoNaoEncontrado_DeveLancarExcecao()
        {
            // Arrange
            int clienteId = _faker.Random.Int(1, 1000);
            int produtoId = _faker.Random.Int(100, 200);

            var cliente = new Cliente(clienteId, _faker.Name.FullName(), _faker.Internet.Email(), _faker.Phone.PhoneNumber());
            var dataPedido = DateTime.Now;

            _produtoRepoMock.Setup(x => x.ObterPorId(produtoId)).Returns((Produto)null);

            var pedido = new Pedido(0, clienteId, cliente, dataPedido);

            // Act
            Action act = () => _pedidoService.CriarPedido(pedido);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Produto não encontrado");

            _pedidoRepoMock.Verify(x => x.Adicionar(It.IsAny<Pedido>()), Times.Never);
        }

        [Fact]
        public void AdicionarProduto_DeveAdicionarProdutoComSucesso()
        {
            // Arrange
            var cliente = new Cliente(1, "João da Silva", "joao@email.com", "1234");
            var pedido = new Pedido(0, cliente.Id, cliente, DateTime.Now);
            var produto = new Produto(1, "Produto A", 50.00m, 10);

            // Act
            pedido.AdicionarItem(produto, 1); // Corrigido

            // Assert
            pedido.Itens.Should().ContainSingle(i => i.Produto == produto); // Corrigido
        }

        [Fact]
        public void CalcularValorTotal_DeveRetornarValorCorreto()
        {
            // Arrange
            var cliente = new Cliente(1, "João", "joao@email.com", "123456789");
            var pedido = new Pedido(0, cliente.Id, cliente, DateTime.Now);

            var produto1 = new Produto(1, "Produto A", 100.00m, 10);
            var produto2 = new Produto(2, "Produto B", 50.00m, 5);

            pedido.AdicionarItem(produto1, 2); // 2 x 100 = 200
            pedido.AdicionarItem(produto2, 3); // 3 x 50 = 150

            // Act
            var total = pedido.CalcularValorTotal();

            // Assert
            total.Should().Be(350.00m);
        }





    }
}
