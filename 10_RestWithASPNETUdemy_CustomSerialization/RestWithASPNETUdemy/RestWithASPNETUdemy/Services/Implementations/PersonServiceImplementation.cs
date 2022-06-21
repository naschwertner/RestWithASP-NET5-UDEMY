using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        //declaração MySQLContext
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        //construtor
        public PersonServiceImplementation(IRepository<Person> repository)//recebendo a injeção
        {
            _repository = repository;  //atribui a variavel ao contex declarado na classe
            _converter = new PersonConverter();
        }
        
        // Método responsável por devolver todas as pessoas,
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        // Método responsável por devolver uma pessoa por ID
        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        // Método responsável por atualizar uma pessoa
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    
    }
}
