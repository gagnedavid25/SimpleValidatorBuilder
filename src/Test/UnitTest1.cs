using Domain.Aggregates;
using Domain.Shared.ValueObjects;
using FluentAssertions;
using Persistence;
using Xunit;

namespace Test
{
    public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			Action act = () => new ChaineRequise("ffgfg");

			act.Should().Throw<NullReferenceException>();
		}

		[Fact]
		public void CodeBonTravail_Test()
		{
			Action act = () => new CodeBonTravail("543");

			act.Should().Throw<NullReferenceException>();
		}

		[Fact]
		public void Quantite_Test()
        {
			decimal value = 10000m;

			var result = Quantite.Creer(value);
        }

		[Fact]
		public void CodeUtilisateur_Test()
        {
			string utilisateur = "003058";

			var result = CodeUtilisateur.Creer(utilisateur);
        }

		[Fact]
		public void NomPays_Test()
        {
			string pays = "  Canada4    ";

			var result = NomPays.Creer(pays);
        }

		[Fact]
		public async Task Test_ajout_utilisateur()
        {
			var codeUtilisateur = CodeUtilisateur.Creer("LUCARS").Value;
			var pays = NomPays.Creer("Canada").Value;
			var cash = Quantite.Creer(2000m).Value;
			var email = "dgagne25@hotmail.com";
			var utilisateur = new Utilisateur(codeUtilisateur, pays, cash, email);

			var dbContext = new GuardDbContext();
			dbContext.Utilisateurs.Add(utilisateur);
			await dbContext.SaveChangesAsync();
        }

		[Fact]
		public async Task Recuperer_utilisateur()
        {
			var dbContext = new GuardDbContext();
			var utilisateur = await dbContext.Utilisateurs.FindAsync(1);
		}
	}
}
