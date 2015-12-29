using System.Security.Principal;

namespace FoodServiceWebApplication.Models.Auth
{
    interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
    }
}