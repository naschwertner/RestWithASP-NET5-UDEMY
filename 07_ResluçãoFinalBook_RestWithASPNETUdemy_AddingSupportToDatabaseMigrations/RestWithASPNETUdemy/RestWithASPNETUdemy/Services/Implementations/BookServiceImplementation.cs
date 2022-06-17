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
    public class BookServiceImplementation : IBookService
    {
        //declaração MySQLContext
        private readonly IBookRepository _repository;

        //construtor
        public BookServiceImplementation(IBookRepository repository)//recebendo a injeção
        {
            _repository = repository;  //atribui a variavel ao contex declarado na classe
        }
        
        // Método responsável por devolver todas as pessoas,
        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        // Método responsável por devolver uma pessoa por ID
        public Book FindByID(long id)
        {
            return _repository.FindByID(id);
        }
        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        // Método responsável por atualizar uma pessoa
        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    
    }
}
