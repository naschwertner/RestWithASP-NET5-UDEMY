using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        // Contador responsável por gerar um ID falso
        // já que ainda não possui acesso ao banco
        private volatile int count;

        // Método responsável por criar uma nova pessoa.
        // Se tivésse um banco de dados esse seria o momento de persistir os dados
        public Person Create(Person person)
        {
            return person;
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            // lógica de exclusão viria aqui
        }

        // Método responsável por devolver todas as pessoas,
        // novamente esta informação é simulada
        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }
            return persons;
        }

        // Método responsável por devolver uma pessoa
        // como não foi acessado nenhum banco de dados esta retornando um mock
        public Person FindByID(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Leandro",
                LastName = "Costa",
                Address = "Uberlandia - Minas Gerais - Brasil",
                Gender = "Male"
            };
        }

        // Método responsável por atualizar uma pessoa
        // sendo mock retorna mesma informação passada
        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Some Address" + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
