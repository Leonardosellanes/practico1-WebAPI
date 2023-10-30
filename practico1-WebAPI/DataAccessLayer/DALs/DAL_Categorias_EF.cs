using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Categorias_EF : IDAL_Categorias
    {
        private DBContextCore _dbContext;

        public DAL_Categorias_EF(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var categoria = _dbContext.Categorias.FirstOrDefault(c => c.Id == id);

            _dbContext.Categorias.Remove(categoria);
            _dbContext.SaveChanges();
        }

        public List<Categoria> Get()
        {
            return _dbContext.Categorias
                .Select(c => new Categoria
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    CategoriaId = c.CategoriaId,
                    empresaId = c.empresaId,
                    Cat_asociada = c.Cat_asociada != null ? new Categoria
                    {
                        Id = c.Cat_asociada.Id,
                        Nombre = c.Cat_asociada.Nombre,
                        empresaId = c.empresaId,
                        CategoriaId = c.Cat_asociada.CategoriaId,
                        Cat_asociada = null
                    } : null
                })
                .ToList();
        }

        public Categoria Get(int id)
        {
            Categorias x = _dbContext.Categorias.FirstOrDefault(x => x.Id == id);

            if (x != null)
            {
                return new Categoria
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    CategoriaId = x.Id,
                    empresaId = x.empresaId,
                    Cat_asociada = new Categoria
                    {
                        Id = x.Cat_asociada.Id,
                        Nombre = x.Cat_asociada.Nombre,
                        CategoriaId = x.Cat_asociada.CategoriaId,
                        Cat_asociada = null
                    }
                };
            }
            else {  
            
                return null;
            }
        }


        public void Insert(Categoria categoria)
        {
            if (categoria != null)
            {
                // Crear una instancia de Categorias a partir de la Categoria
                Categorias categoriaEntity = new Categorias
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre,
                    CategoriaId = categoria.CategoriaId,
                    empresaId = categoria.empresaId,
                    Cat_asociada = null
                };

                _dbContext.Categorias.Add(categoriaEntity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("categoria", "La categoría no puede ser nula.");
            }
        }

        public void Update(Categoria categoria)
        {
            if (categoria != null)
            {
                
                var categoriaExistente = _dbContext.Categorias.FirstOrDefault(c => c.Id == categoria.Id);

                if (categoriaExistente != null)
                {
                   
                    categoriaExistente.Nombre = categoria.Nombre;
                    categoriaExistente.CategoriaId = categoria.CategoriaId;
                    
                    _dbContext.SaveChanges();
                }
                else
                {
                   
                    throw new Exception($"No se encontró una categoría con el ID {categoria.Id}");
                }
            }
            else
            {
               
                throw new ArgumentNullException("categoria", "La categoría no puede ser nula.");
            }
        }


    }
}
