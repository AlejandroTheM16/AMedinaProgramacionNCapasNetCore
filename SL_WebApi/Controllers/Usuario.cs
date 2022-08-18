using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers

{

    [ApiController]
   

    public class Usuario : ControllerBase
    {
        [HttpPost]
        [Route("api/Usuario/Add")]
        public IActionResult Add([FromBody]ML.Usuario usuario)
        {
            var result = BL.Usuario.AddEF(usuario);
            if (result.Correct) {

                return Ok(result);

            }
            else {

                return NotFound(result);
            
            }
           
        }

        [HttpPost]
        [Route("api/Usuario/Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.UpdateEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpPost]
        [Route("api/Usuario/GetAll")]
        public IActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            var result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {

                return Ok(result);

            }
            else
            {

                return NotFound(result);

            }

        }


        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            var result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {

                return Ok(result);

            }
            else
            {

                return NotFound(result);

            }

        }

        [HttpGet]
        [Route("api/Usuario/GetById")]
        public IActionResult GetById(int Id_Usuario)
        {
            

            var result = BL.Usuario.GetByIdEF(Id_Usuario);

            if (result.Correct)
            {

                return Ok(result);

            }
            else
            {

                return NotFound(result);

            }

        }

        [HttpGet]
        [Route("api/Usuario/Delete")]
        public IActionResult Delete(int IdUsuario)
        {           

            var result = BL.Usuario.DeleteEF(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);

            }

        }
    }
}
