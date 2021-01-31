using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplicationTest.Models;
using WebApplicationTest.Repository;
using WebApplicationTest.Context;
using Swashbuckle.Swagger.Annotations;

namespace WebApplicationTest.Controllers
{
    public class TestApiController : ApiController
    {

        IRep<Player> player;


        public TestApiController(IRep<Player> players)
        {
            this.player = players;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet]
        [Route("players/all")]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await player.GetAll();
        }

        [HttpGet]
        [Route("player/ById")]
        public async Task<Player> GetPlayerId(int? Id)
        {
            return await player.Get(Id);
        }


        [HttpPost]
        [Route("player/create")]       
        public  async Task<IHttpActionResult> Create(Player player, string Name)
        {
            if (player == null)
            {
                return BadRequest();
            }
            
            if(ModelState.IsValid)
            {
                await this.player.Create(player, Name);

                return Ok();
            }

            return BadRequest();

        }
       
        [HttpPut]
        [Route("player/update")]
        public async Task<IHttpActionResult> Update(Player player)
        {
            if ( player == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.player.Update(player);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("player/delete")]
        public async Task<IHttpActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               await this.player.Delete(id);

                return Ok();
            }

            return BadRequest();
        }


    }
}
