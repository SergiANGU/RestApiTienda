using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiTienda.Controllers;
using RestApiTienda.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
namespace RestApiTienda.UnitTest
{
    public class ProductoControllerTests
    {

        [Fact]
        public void Crear_DebeRetornarOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TiendaRopaContext>()
            .UseInMemoryDatabase(databaseName: "BBDDTest")
            .Options;

            var dbContext = new TiendaRopaContext(options);

            var controller = new ProductoController(dbContext);
            var newProducto = new Producto { Talla = "L", Color = "Verd", Precio = 12, Descripcion = "Pull" };

            // Act
            var result = controller.Crear(newProducto) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            
        }

        [Fact]
        public void Editar_DebeRetornarOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TiendaRopaContext>()
            .UseInMemoryDatabase(databaseName: "BBDDTest")
            .Options;

            var dbContext = new TiendaRopaContext(options);

            var controller = new ProductoController(dbContext);
            var existingProducto = new Producto { Id = 1 };
            var updatedProducto = new Producto { Id = 1, Talla = "M", Color = "Rojo", Precio = 9.99, Descripcion = "Descripción actualizada" };

            dbContext.Productos.Add(existingProducto); // Agregar el producto existente al DbContext para pruebas

            // Act
            var result = controller.Editar(updatedProducto) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            
            // Verificar que los cambios se hayan guardado correctamente en el DbContext
            Assert.Equal(updatedProducto.Talla, existingProducto.Talla);
            Assert.Equal(updatedProducto.Color, existingProducto.Color);
            Assert.Equal(updatedProducto.Precio, existingProducto.Precio);
            Assert.Equal(updatedProducto.Descripcion, existingProducto.Descripcion);
        }

        [Fact]
        public void Eliminar_DebeRetornarOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TiendaRopaContext>()
            .UseInMemoryDatabase(databaseName: "BBDDTest")
            .Options;

            var dbContext = new TiendaRopaContext(options);

            var controller = new ProductoController(dbContext);
            var existingProducto = new Producto { Id = 1 }; 

            dbContext.Productos.Add(existingProducto);

            // Act
            var result = controller.Eliminar(1) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            

            // Verificar que el producto haya sido eliminado del DbContext
            var deletedProducto = dbContext.Productos.Find(1);
            Assert.Null(deletedProducto);
        } 
    }    
}
