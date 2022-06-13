using Microsoft.EntityFrameworkCore.Internal;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        //declaração MySQLContext
        private MySQLContext _context;

        //construtor
        public PersonServiceImplementation(MySQLContext context)//recebendo a injeção
        {
            _context = context;  //atribui a variavel ao contex declarado na classe
        }
        
        // Método responsável por devolver todas as pessoas,
        public List<Person> FindAll()
        {
            return _context.People.ToList();
        }

        // Método responsável por devolver uma pessoa por ID
        public Person FindByID(long id)
        {
            return _context.People.SingleOrDefault(p => p.Id.Equals(id));
        }
        public Person Create(Person person)
        {
            //esse try catch salva  objeto depois retorna ele 
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;      
            }
            return person;
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();


            var result = _context.People.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null){

                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                    
            }
            return person;
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            var result = _context.People.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {

                try
                {
                    _context.People.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

    
        private bool Exists(long id)
        {
            return _context.People.Any(p => p.Id.Equals(id));
        }
    }
}
