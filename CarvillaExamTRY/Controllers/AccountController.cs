﻿using CarvillaExamTRY.Models;
using CarvillaExamTRY.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarvillaExamTRY.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.UserName,
            };
            var result = await userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
            }
            
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
           if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindByEmailAsync(vm.UserNameOrEmail);
            if (user == null)
            {
                user = await userManager.FindByNameAsync(vm.UserNameOrEmail);
                if(user == null)
                {
                    throw new Exception("Username/EmailAdress or Passwor is incorrect.");
                }
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, vm.Password, false);
            if (!result.Succeeded)
            {
                throw new Exception("Username/EmailAdress or Passwor is incorrect.");
            }
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
