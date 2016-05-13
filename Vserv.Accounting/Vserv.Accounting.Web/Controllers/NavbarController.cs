﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Web.Models;

namespace Vserv.Accounting.Web.Controllers
{
    public class NavbarController : Controller
    {
        //
        // GET: /Navbar/
        public ActionResult Index()
        {
            return PartialView("_Navbar", navbarItems().ToList());
        }

        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar
            {
                Id = 1,
                nameOption = "Dashboard",
                controller = "Dashboard",
                action = "Index",
                imageClass = "fa fa-dashboard fa-fw",
                status = true,
                isParent = false,
                parentId = 0
            });

            menu.Add(new Navbar
            {
                Id = 2,
                nameOption = "Manage Employees",
                imageClass = "fa fa-users fa-fw",
                status = true,
                isParent = true,
                parentId = 0
            });
            menu.Add(new Navbar
            {
                Id = 3,
                nameOption = "View Employees",
                controller = "Employee",
                action = "List",
                status = true,
                isParent = false,
                parentId = 2
            });
            menu.Add(new Navbar
            {
                Id = 4,
                nameOption = "Add Employee",
                controller = "Employee",
                action = "Add",
                status = true,
                isParent = false,
                parentId = 2
            });
            //menu.Add(new Navbar
            //{
            //    Id = 5,
            //    nameOption = "Test Add",
            //    controller = "Employee",
            //    action = "TestAdd",
            //    status = true,
            //    isParent = false,
            //    parentId = 2
            //});

            menu.Add(new Navbar
            {
                Id = 6,
                nameOption = "Settings",
                imageClass = "fa fa-gear fa-fw",
                status = true,
                isParent = true,
                parentId = 0
            });

            menu.Add(new Navbar
            {
                Id = 7,
                nameOption = "Change Password",
                controller = "Account",
                action = "ChangePassword",
                status = true,
                isParent = false,
                parentId = 6
            });
            menu.Add(new Navbar
            {
                Id = 7,
                nameOption = "My Profile",
                controller = "Account",
                action = "profile",
                status = true,
                isParent = false,
                parentId = 6
            });
            return menu.ToList();
        }
    }
}