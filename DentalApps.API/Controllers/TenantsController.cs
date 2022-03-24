using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalApps.API.DAL.Interface;
using DentalApps.Models;
using DentalApps.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DentalApps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenants _tenant;
        private readonly IMapper _mapper;

        public TenantsController(ITenants tenant,IMapper mapper)
        {
            _tenant = tenant;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantReadDto>>> GetAll()
        {
            var results = await _tenant.GetAll(); 

            var output = _mapper.Map<IEnumerable<TenantReadDto>>(results);
            return Ok(output);
        }

        [HttpGet("{tenantId}",Name = "GetTenantById")]
        public async Task<ActionResult<TenantReadDto>> GetTenantById(string tenantId)
        {
            var tenant = await _tenant.GetById(tenantId);
            if(tenant==null) return NotFound();

            var output = _mapper.Map<TenantReadDto>(tenant);
            return Ok(output);
        }


        [HttpPost]
        public async Task<ActionResult<TenantReadDto>> Create(TenantCreateDto tenantCreateDto)
        {
            try
            {
                var newTenant = _mapper.Map<Tenant>(tenantCreateDto);
                await _tenant.Create(newTenant);
                var tenantReadDto = _mapper.Map<TenantReadDto>(newTenant);
                return CreatedAtRoute(nameof(GetTenantById), 
                    new { tenantId=tenantReadDto.TenantID },tenantReadDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}