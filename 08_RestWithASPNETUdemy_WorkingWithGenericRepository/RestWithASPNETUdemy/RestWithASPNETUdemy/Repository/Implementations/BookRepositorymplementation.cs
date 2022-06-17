using Microsoft.EntityFrameworkCore.Internal;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        //declaração MySQLContext
        private MySQLContext _context;

        //construtor
        public BookRepositoryImplementation(MySQLContext context)//recebendo a injeção
        {
            _context = context;  //atribui a variavel ao contex declarado na classe
        }
        
        // Método responsável por devolver todas as pessoas,
        public List<Book> FindAll()
        {
            return _context.Book.ToList();
        }

        // Método responsável por devolver uma pessoa por ID
        public Book FindByID(long id)
        {
            return _context.Book.SingleOrDefault(p => p.Id.Equals(id));
        }
        public Book Create(Book book)
        {
            //esse try catch salva  objeto depois retorna ele 
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;      
            }
            return book;
        }

        // Método responsável por atualizar uma pessoa
        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;


            var result = _context.Book.SingleOrDefault(p => p.Id.Equals(book.Id));
            if (result != null){

                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                    
            }
            return book;
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            var result = _context.Book.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {

                try
                {
                    _context.Book.Remove(result);
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
            return _context.Book.Any(p => p.Id.Equals(id));
        }
    }
}
