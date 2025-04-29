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
    public class ClienteServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Faker _faker;
        private readonly Mock<IClienteRepository> _clienteRepoMock;
        private readonly ClienteService _clienteService;

        public ClienteServiceTests()
        {
            _fixture = new Fixture();
            _faker = new Faker("pt_BR");
            _clienteRepoMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepoMock.Object);
        }

        [Theory]
        [InlineData("João da Silva", "joao@email.com")]
        [InlineData("Maria Souza", "maria.souza@gmail.com")]
        public void AdicionarCliente_Valido_DeveAdicionarComSucesso(string nome, string email)
        {
            // Arrange
            var id = _faker.Random.Int(1, 1000);
            var telefone = _faker.Phone.PhoneNumber();
            var cliente = new Cliente(id, nome, email, telefone);
            _clienteRepoMock.Setup(x => x.EmailExiste(email)).Returns(false);

            // Act
            var resultado = _clienteService.AdicionarCliente(cliente);

            // Assert
            resultado.Should().BeTrue();
            cliente.Nome.Should().Be(nome);
            cliente.Email.Should().Be(email);
            cliente.Telefone.Should().Be(telefone);
            _clienteRepoMock.Verify(x => x.Adicionar(cliente), Times.Once);
        }

        [Fact]
        public void AdicionarCliente_EmailDuplicado_DeveLancarExcecao()
        {
            // Arrange
            var emailDuplicado = _faker.Internet.Email();
            var nome = _faker.Name.FullName();
            var cliente = new Cliente(_faker.Random.Int(1, 1000), nome, emailDuplicado, _faker.Phone.PhoneNumber());
            _clienteRepoMock.Setup(x => x.EmailExiste(emailDuplicado)).Returns(true);

            // Act
            Action act = () => _clienteService.AdicionarCliente(cliente);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("E-mail já cadastrado");
            cliente.Email.Should().Be(emailDuplicado);
            cliente.Nome.Should().NotBeNullOrEmpty();
            _clienteRepoMock.Verify(x => x.Adicionar(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public void AdicionarCliente_EmailInvalido_DeveLancarExcecao()
        {
            // Arrange
            var cliente = new Cliente(1, _faker.Name.FullName(), "emailinvalido", _faker.Phone.PhoneNumber());

            // Act
            Action act = () => _clienteService.AdicionarCliente(cliente);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("E-mail inválido");
            cliente.Email.Should().Contain("emailinvalido");
            cliente.Nome.Should().NotBeNull();
            _clienteRepoMock.Verify(x => x.Adicionar(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
