﻿using Criptic.Core.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Criptic.Core.Attributes;

public class AuthorizeMultiplePolicyAttribute:TypeFilterAttribute
{
    public AuthorizeMultiplePolicyAttribute(string policies, bool IsAll):
        base(typeof(AuthorizeMultiplePolicyFilter))
    {
        Arguments = new object[] { policies, IsAll };
    }
}