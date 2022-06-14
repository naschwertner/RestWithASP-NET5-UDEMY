using Microsoft.EntityFrameworkCore.Internal;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        //declaração MySQLContext
        private readonly IPersonRepository _repository;

        //construtor
        public PersonServiceImplementation(IPersonRepository repository)//recebendo a injeção
        {
            _repository = repository;  //atribui a variavel ao contex declarado na classe
        }
        
        // Método responsável por devolver todas as pessoas,
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        // Método responsável por devolver uma pessoa por ID
        public Person FindByID(long id)
        {
            return _repository.FindByID(id);
        }
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    
    }
}
