using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    //DbContext (muitas vezes referida como o contexto). Essa classe de contexto administra os objetos entidades
    //durante o tempo de execução, o que inclui preencher objetos com dados de um banco de dados, controlar alterações,
    //e persistir dados para o banco de dados.
    public class MySQLContext : DbContext 
    {
        public MySQLContext()  //construtor
        {

        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { } //passa as options para o base

        public DbSet<Person> People { get; set; } //lista de pessoas
    }
}
