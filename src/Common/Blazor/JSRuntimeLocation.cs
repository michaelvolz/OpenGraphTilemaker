using System;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.AspNetCore.Blazor
{
    public static class JsRuntimeLocation
    {
        public static bool HasMono => Type.GetType("Mono.Runtime") != null;
        public static bool IsClientSide => HasMono;
        public static bool IsServerSide => !HasMono;
    }
}
