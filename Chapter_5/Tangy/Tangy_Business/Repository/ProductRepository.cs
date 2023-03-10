using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tandy_DataAccess;
using Tandy_DataAccess.Data;
using Tangy_Business.Repository.IRepository;
using Tangy_Models;

namespace Tangy_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var obj = _mapper.Map<ProductDTO, Product>(objDTO);
            var addedObj = _db.Products.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                _db.Products.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var obj = await _db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            //await Task.Delay(1000); //Этого нет у переводчика
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await _db.Products.Include(x => x.Category).ToListAsync());
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var objFromDb = await _db.Products.FirstOrDefaultAsync(x => x.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ShopFavorites= objDTO.ShopFavorites;
                objFromDb.CustomerFavorites= objDTO.CustomerFavorites;
                objFromDb.Color= objDTO.Color;
                objFromDb.ImageUrl= objDTO.ImageUrl;
                objFromDb.CategoryId= objDTO.CategoryId;
                //Этой строки нет у переводчика
                //objFromDb.Category = _mapper.Map<CategoryDTO, Category>(objDTO.Category);
                _db.Products.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}
