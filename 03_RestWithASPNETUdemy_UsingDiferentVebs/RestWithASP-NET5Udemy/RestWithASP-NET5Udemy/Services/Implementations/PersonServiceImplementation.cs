using RestWithASP_NET5Udemy.Model;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASP_NET5Udemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
       // Contator responsavel por gerar ID fakes
       //desde que não esteja acessando nenhum banco 
        private volatile int count; //para mockar um id

        //metodo responsavel por criar uma nova pessoa
        //Se tivéssemos um banco de dados esse seria o momento de persistir os dados
        public Person Create(Person person)
        {
            return person;
        }
        //metodo responsavel por deletar uma pessoa pelo ID
        public void Delete(long id)
        {
            //a lógica vai vir aqui 
        }

        //metodo responsavel por retornar todas as pessoas
        //info mockada
        public List<Person> FindAll()
        {
            List<Person> people = new List<Person> ();
            for(int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }
            return people;
        }

        
        //metodo responsavel por devolver uma pessoa
        //como ainda não esta acessando o banco, retorna um mock
        public Person FindByID(long id)
        {
            return new Person
            {
                Id = IncrementAndGet(), //toda vez que for chamado incrementa 1 no id 
                FirstName = "Leandro",
                LastName = "Costa",
                Address = "Uberlandia - Minas Gerais - Brasil",
                Gender = "Male"
            };
        }

        // Método responsável por atualizar uma pessoa para
        // sendo mock retornamos a mesma informação passada
        public Person Update(Person person)
        {
            return person;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name" +i,
                LastName = "Person Lastname" +i,
                Address = "Some Address" +i,
                Gender = "Male"
            };
        }

        private object IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
