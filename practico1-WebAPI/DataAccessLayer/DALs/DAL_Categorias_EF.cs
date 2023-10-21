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
            throw new NotImplementedException();
            //var categoria = _dbContext.Categorias.FirstOrDefault(c => c.Id == id);

            //if (categoria != null)
            //{
               
            //    if (categoria.Productos.Any())
            //    {
            //        throw new Exception($"No se puede eliminar la categoría con ID {id} porque tiene productos asociados.");
            //    }

                
            //    _dbContext.Categorias.Remove(categoria);
            //    _dbContext.SaveChanges();
            //}
            //else
            //{
            //    throw new Exception($"No se encontró una categoría con el ID {id}");
            //}
        }

        public List<Categoria> Get()
        {
            return _dbContext.Categorias
                .Include(c => c.Cat_asociadas)
                .Select(c => new Categoria
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Cat_asociadas = c.Cat_asociadas.Select(ca => new Categoria
                    {
                        Id = ca.Id,
                        Nombre = ca.Nombre
                    }).ToList()
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
                    Cat_asociadas = x.Cat_asociadas.Select(ca => new Categoria
                    {
                        Id = ca.Id,
                        Nombre = ca.Nombre
                    }).ToList()
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
                    Cat_asociadas = categoria.Cat_asociadas?.Select(ca => new Categorias
                    {
                        Id = ca.Id,
                        Nombre = ca.Nombre
                    }).ToList()
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
