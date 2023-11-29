﻿using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EFModels;
using Shared;
using System.Buffers.Text;

namespace DataAccessLayer.DALs
{
    public class DAL_OC : IDAL_OC
    {
        private DBContextCore _dbContext;

        public DAL_OC(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public Orden ObtenerOCPorId(long id)
        {
            OC o = _dbContext.OC
                .Include(oc => oc.CarritoProducto)
                .FirstOrDefault(oc => oc.Id == id);

            return o == null
                ? null
                : new Orden
                {
                    Id = o.Id,
                    MedioDePago = o.MedioDePago,
                    DireccionDeEnvio = o.DireccionDeEnvio,
                    FechaEstimadaEntrega = o.FechaEstimadaEntrega,
                    Total = o.Total,
                    EstadoOrden = o.EstadoOrden,
                    Fecha = o.Fecha,
                    Carritos = o.CarritoProducto != null ? o.CarritoProducto.Select(cp => new Carrito
                    {
                        Id = cp.Id,
                        Cantidad = cp.Cantidad,
                        POs = cp.POs != null ? new Producto
                        {
                            Id = cp.POs.Id,
                            Titulo = cp.POs.Titulo,
                            Descripcion = cp.POs.Descripcion,
                            Foto = cp.POs.Foto,
                            Precio = cp.POs.Precio,
                            Tipo_iva = cp.POs.Tipo_iva,
                            Pdf = cp.POs.Pdf,
                            EmpresaId = cp.POs.EmpresaId,
                            CategoriaId = cp.POs.CategoriaId,
                            Categoria = cp.POs.CategoriaAsociada != null ? new Categoria
                            {
                                Id = cp.POs.CategoriaAsociada.Id,
                                Nombre = cp.POs.CategoriaAsociada.Nombre
                            } : null
                        } : null
                    }).ToList() : null
                };
        }


        public Orden obtenerCarrito(string clienteId)
        {
            OC oc = _dbContext.OC
                .Include(o => o.CarritoProducto)
                .ThenInclude(cp => cp.POs)
                .FirstOrDefault(o => o.Cliente.Id == clienteId && o.EstadoOrden == "activo");

            Orden carrito;

            if (oc == null)
            {
                ApplicationUser Cliente = _dbContext.Usuarios.Find(clienteId);

                if (Cliente != null) {

                    var nuevoCarritoEF = new OC
                    {
                        Cliente = Cliente,
                        EstadoOrden = "activo",
                    };

                    _dbContext.OC.Add(nuevoCarritoEF);
                    _dbContext.SaveChanges();

                    carrito = new Orden
                    {
                        Id = nuevoCarritoEF.Id,
                        MedioDePago = nuevoCarritoEF.MedioDePago,
                        DireccionDeEnvio = nuevoCarritoEF.DireccionDeEnvio,
                        FechaEstimadaEntrega = nuevoCarritoEF.FechaEstimadaEntrega,
                        Total = nuevoCarritoEF.Total,
                        EstadoOrden = nuevoCarritoEF.EstadoOrden,
                        Fecha = nuevoCarritoEF.Fecha,
                        ClienteId = Cliente.Id
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                carrito = new Orden
                {
                    Id = oc.Id,
                    MedioDePago = oc.MedioDePago,
                    DireccionDeEnvio = oc.DireccionDeEnvio,
                    FechaEstimadaEntrega = oc.FechaEstimadaEntrega,
                    Total = oc.Total,
                    EstadoOrden = oc.EstadoOrden,
                    Fecha = oc.Fecha,
                    Carritos = oc.CarritoProducto != null
                        ? oc.CarritoProducto.Select(cp => new Carrito
                        {
                            Id = cp.Id,
                            Cantidad = cp.Cantidad,
                            ProductoId = cp.ProductoId,
                            OCId = oc.Id,
                            POs = cp.POs != null
                                ? new Producto
                                {
                                    Id = cp.POs.Id,
                                    Titulo = cp.POs.Titulo,
                                    Descripcion = cp.POs.Descripcion,
                                    Foto = cp.POs.Foto,
                                    Base64 = GetImage(cp.POs.Foto),
                                    Precio = cp.POs.Precio,
                                    Tipo_iva = cp.POs.Tipo_iva,
                                    Pdf = cp.POs.Pdf,
                                    Base64pdf = GetPdf(cp.POs.Pdf),
                                    EmpresaId = cp.POs.EmpresaId,
                                    CategoriaId = cp.POs.CategoriaId,
                                    Categoria = cp.POs.CategoriaAsociada != null
                                          ? new Categoria
                                          {
                                              Id = cp.POs.CategoriaAsociada.Id,
                                              Nombre = cp.POs.CategoriaAsociada.Nombre
                                          }
                                          : null
                                }
                                : null
                        }).ToList()
                        : null
                };
            }

            return carrito;

        }

        public List<Orden> ObtenerOCPorUserId(string id)
        {
            return _dbContext.OC
                .Include(oc => oc.CarritoProducto)
                .Include(rc => rc.Rcs)
                .Where(oc => oc.Cliente.Id == id)
                .Select(oc => new Orden
                {
                    Id = oc.Id,
                    MedioDePago = oc.MedioDePago,
                    DireccionDeEnvio = oc.DireccionDeEnvio,
                    FechaEstimadaEntrega = oc.FechaEstimadaEntrega,
                    Total = oc.Total,
                    EstadoOrden = oc.EstadoOrden,
                    Fecha = oc.Fecha,
                    Carritos = oc.CarritoProducto != null ? oc.CarritoProducto.Select(cp => new Carrito
                    {
                        Id = cp.Id,
                        Cantidad = cp.Cantidad,
                        POs = new Producto
                        {
                            Id = cp.POs.Id,
                            Titulo = cp.POs.Titulo,
                            Descripcion = cp.POs.Descripcion,
                            Foto = cp.POs.Foto,
                            Base64 = GetImage(cp.POs.Foto),
                            Precio = cp.POs.Precio,
                            Tipo_iva = cp.POs.Tipo_iva,
                            Pdf = cp.POs.Pdf,
                            EmpresaId = cp.POs.EmpresaId,
                            CategoriaId = cp.POs.CategoriaId,
                            Categoria = new Categoria
                            {
                                Id = cp.POs.CategoriaAsociada.Id,
                                Nombre = cp.POs.CategoriaAsociada.Nombre
                            }
                        }
                    }).ToList() : new List<Carrito>(),
                    Rcs = oc.Rcs != null ? new Reclamo
                    {
                        Id = oc.Rcs.Id,
                        Fecha = oc.Rcs.Fecha,
                        Descripcion = oc.Rcs.Descripcion

                    } : null
                })
                .ToList();  
        }

        public List<Orden> ObtenerOCPorEmpresaId(int id)
        {
            return _dbContext.OC
                .Include(oc => oc.CarritoProducto)
                .Include(rc => rc.Rcs)
                .Where(oc => oc.EmpresaId == id)
                .Select(oc => new Orden
                {
                    Id = oc.Id,
                    MedioDePago = oc.MedioDePago,
                    DireccionDeEnvio = oc.DireccionDeEnvio,
                    FechaEstimadaEntrega = oc.FechaEstimadaEntrega,
                    Total = oc.Total,
                    EstadoOrden = oc.EstadoOrden,
                    Fecha = oc.Fecha,
                    ClienteId = oc.ClienteId,
                    EmpresaId = oc.EmpresaId,
                    SucursalId = oc.SucursalId,
                    Carritos = oc.CarritoProducto != null ? oc.CarritoProducto.Select(cp => new Carrito
                    {
                        Id = cp.Id,
                        Cantidad = cp.Cantidad,
                        POs = new Producto
                        {
                            Id = cp.POs.Id,
                            Titulo = cp.POs.Titulo,
                            Descripcion = cp.POs.Descripcion,
                            Foto = cp.POs.Foto,
                            Base64 = GetImage(cp.POs.Foto),
                            Precio = cp.POs.Precio,
                            Tipo_iva = cp.POs.Tipo_iva,
                            Pdf = cp.POs.Pdf,
                            EmpresaId = cp.POs.EmpresaId,
                            CategoriaId = cp.POs.CategoriaId,
                            Categoria = new Categoria
                            {
                                Id = cp.POs.CategoriaAsociada.Id,
                                Nombre = cp.POs.CategoriaAsociada.Nombre
                            }
                        }
                    }).ToList() : new List<Carrito>(),
                    Rcs = oc.Rcs != null ? new Reclamo
                    {
                        Id = oc.Rcs.Id,
                        Fecha = oc.Rcs.Fecha,
                        Descripcion = oc.Rcs.Descripcion

                    } : null
                })
                .ToList();
        }

        public void CrearOC(Orden orden)
        {

            _dbContext.OC.Add(
                new OC
                {
                    MedioDePago = orden.MedioDePago,
                    DireccionDeEnvio = orden.DireccionDeEnvio,
                    FechaEstimadaEntrega = orden.FechaEstimadaEntrega,
                    Total = orden.Total,
                    EstadoOrden = orden.EstadoOrden,
                    Fecha = orden.Fecha,
                    EmpresaId = orden.EmpresaId,
                    ClienteId = orden.ClienteId,
                }
                );
            _dbContext.SaveChanges();
        }

        public void ActualizarOC(Orden orden)
        {
            OC oc = _dbContext.OC.FirstOrDefault(o => o.Id == orden.Id);

            if (oc != null)
            {
                oc.MedioDePago = orden.MedioDePago;
                oc.DireccionDeEnvio = orden.DireccionDeEnvio;
                oc.FechaEstimadaEntrega = orden.FechaEstimadaEntrega;
                oc.Total = orden.Total;
                oc.EstadoOrden = orden.EstadoOrden;
                oc.Fecha = orden.Fecha;

                _dbContext.SaveChanges();
            }
        }

        public void EliminarOC(long id)
        {
            OC orden = _dbContext.OC.Find(id);
            if (orden != null)
            {
                _dbContext.OC.Remove(orden);
                _dbContext.SaveChanges();
            }
        }

        public static string GetImage(string fileName)
        {
            string filePath = Path.Combine("Archivos", "Imagenes", fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    return Convert.ToBase64String(fileBytes);
                }
                catch (Exception ex)
                {
                    return $"Error al leer el archivo: {ex.Message}";
                }
            }
            else
            {
                return filePath;
            }
        }

        public static string GetPdf(string fileName)
        {
            string filePath = Path.Combine("Archivos", "Pdf", fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    return Convert.ToBase64String(fileBytes);
                }
                catch (Exception ex)
                {
                    return $"Error al leer el archivo: {ex.Message}";
                }
            }
            else
            {
                return "El archivo no fue encontrado.";
            }
        }
    }
}
