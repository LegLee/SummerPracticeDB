using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using SummerPracticeDB.Models;

namespace SummerPracticeDB.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly string ConnectionString = "User ID=postgres;Password=1592648;Host=localhost;Port=5432;Database=practicedatabase;";
        private IDbConnection con;
        public IActionResult Index()
        {
            try
            {
                string selectQuery = "SELECT * FROM departments";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                IEnumerable<Departments> listDepartments = con.Query<Departments>(selectQuery).ToList();
                return View(listDepartments);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Departments departments)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string insertQuery = "INSERT INTO departments (name, chiefs_id) VALUES (@name, @chiefs_id)";
                    con = new NpgsqlConnection(ConnectionString);
                    con.Open();
                    con.Execute(insertQuery, departments);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return View(departments);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                string selectQuery = "SELECT * FROM departments WHERE id = @id";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                Departments departments = con.Query<Departments>(selectQuery, new { id = id }).FirstOrDefault();
                return View(departments);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        [HttpPost]
        public IActionResult Update(int id, Departments departments)
        {
            if (id != departments.id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    string updateQuery = "UPDATE departments SET name=@name, chiefs_id=@chiefs_id WHERE id=@id";
                    con = new NpgsqlConnection(ConnectionString);
                    con.Open();
                    con.Execute(updateQuery, departments);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    con.Close();
                }

            }
            return View(departments);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM departments WHERE id=@id";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                con.Query(deleteQuery, new { id = id });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        [HttpPost]
        public IActionResult Search(int id)
        {
            try
            {
                string searchQuery = "SELECT * FROM departments WHERE id=@id";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                Departments departments = con.Query<Departments>(searchQuery, new { id = id }).FirstOrDefault();
                return View(departments);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Search()
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SearchTab()
        {
            return View();
        }
    }
}
