using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;



namespace RestWithASPNETUdemy.Services.Implementations
{
    public class BookServiceImplementation : IBookService
    {
        //declaração MySQLContext
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        //construtor
        public BookServiceImplementation(IRepository<Book> repository)//recebendo a injeção
        {
            _repository = repository;  //atribui a variavel ao contex declarado na classe
            _converter = new BookConverter();
        }
        
        // Método responsável por devolver todas as pessoas,
        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());

        }

        // Método responsável por devolver uma pessoa por ID
        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }
        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity); ;
        }

        // Método responsável por atualizar uma pessoa
        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        // Método responsável por excluir uma pessoa de um ID
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    
    }
}
