using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("api/procesarpago")]
public class VerificacionController : ControllerBase
{
    [HttpPost]
    public IActionResult VerifyCard([FromBody] string cardNumber)
    {
        string cardEnding = cardNumber[^2..];

        switch (cardEnding)
        {
            case "11":
                return BadRequest("Saldo insuficiente.");
            case "22":
                return BadRequest("Tarjeta vencida.");
            case "33":
                return BadRequest("Código de verificación incorrecto.");
            case "44":
                // Simula un tiempo de espera (timeout)
                System.Threading.Thread.Sleep(10000); // 10 segundos de espera
                return StatusCode(StatusCodes.Status504GatewayTimeout, "Timeout");
            default:
                return Ok("Transacción exitosa.");
        }
    }
}

