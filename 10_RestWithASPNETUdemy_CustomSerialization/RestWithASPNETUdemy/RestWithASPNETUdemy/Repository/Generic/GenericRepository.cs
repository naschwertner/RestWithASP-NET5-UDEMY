﻿using RestWithASPNETUdemy.Model.Base;
using System.Collections.Generic;
using System;
using RestWithASPNETUdemy.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        //declaração MySQLContext
        private MySQLContext _context;

        private DbSet<T> dataset;

        //construtor
        public GenericRepository(MySQLContext context)//recebendo a injeção
        {
            _context = context;  //atribui a variavel ao contex declarado na classe
            dataset = _context.Set<T>();
        }
      

      
        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindByID(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        } 
        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            else
            {
                return null;
            }
        }
        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            try
            {
                dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

    }
}
