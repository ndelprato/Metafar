﻿using System.Net;

namespace Metafar.Core;

public class RespuestaApi
{
    public RespuestaApi()
    {
        ErrorMessages = new List<string>();
    }

    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string> ErrorMessages { get; set; }
    public object Result { get; set; }
    

}
