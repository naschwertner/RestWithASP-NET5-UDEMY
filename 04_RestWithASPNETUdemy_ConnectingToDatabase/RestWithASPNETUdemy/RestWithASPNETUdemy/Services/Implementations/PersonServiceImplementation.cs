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
        
        private MySQLContext _context;

        public PersonServiceImplementation(MySQLContext context)
        {
            _context = context;
        }
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
            return _context.Pople.ToList();
        }

        // Método responsável por devolver uma pessoa
        // como não foi acessado nenhum banco de dados esta retornando um mock
        public Person FindByID(long id)
        {
            return _context.Pople.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Método responsável por atualizar uma pessoa
        // sendo mock retorna mesma informação passada
        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();


            var result = _context.Pople.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            try
                {
                    _context.Entry(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                return person;
        }

        private bool Exists(long id)
        {
            return _context.Pople.Any(p => p.Id.Equals(id));
        }
    }
}
