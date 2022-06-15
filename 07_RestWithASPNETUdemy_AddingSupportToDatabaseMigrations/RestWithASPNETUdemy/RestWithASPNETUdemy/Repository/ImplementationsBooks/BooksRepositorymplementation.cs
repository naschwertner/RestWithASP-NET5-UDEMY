using Microsoft.EntityFrameworkCore.Internal;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BooksRepositoryImplementation : BooksRepository
    {
        //declaração MySQLContext
        private MySQLContext _context;

        //construtor
        public PersonRepositoryImplementation(MySQLContext context)//recebendo a injeção
        {
            _context = context;  //atribui a variavel ao contex declarado na classe
        }
        
        
        public List<Books> FindAll()
        {
            return _context.Books.ToList();
        }

        
        public Books FindByID(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }
        public Books Create(Books books)
        {
            //esse try catch salva  objeto depois retorna ele 
            try
            {
                _context.Add(books);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;      
            }
            return books;
        }

        // Método responsável por atualizar uma pessoa
        public Books Update(Books books)
        {
            if (!Exists(books.Id)) return null;


            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(books.Id));
            if (result != null){

                try
                {
                    _context.Entry(result).CurrentValues.SetValues(books);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                    
            }
            return books;
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {

                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

    
        public bool Exists(long id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }
    }
}
