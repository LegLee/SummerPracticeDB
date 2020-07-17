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
    public class StuffContoller : Controller
    {
        private readonly string ConnectionString = "User ID=postgres;Password=1592648;Host=localhost;Port=5432;Database=practicedatabase;";
        private IDbConnection con;
        public IActionResult Index()
        {
            try
            {
                string selectQuery = "SELECT * FROM stuff";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                IEnumerable < Stuff > listStuff = con.Query<Stuff>(selectQuery).ToList();
                return View(listStuff);
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
        public IActionResult Create(Stuff stuff)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string insertQuery = "INSERT INTO stuff (name, id_dep) VALUES (@name, @id_dep)";
                    con = new NpgsqlConnection(ConnectionString);
                    con.Open();
                    con.Execute(insertQuery, stuff);
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
            return View(stuff);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                string selectQuery = "SELECT * FROM stuff WHERE id = @id";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                Stuff stuff = con.Query<Stuff>(selectQuery, new { id = id }).FirstOrDefault();
                return View(stuff);
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
        public IActionResult Update(int id, Stuff stuff)
        {
            if (id != stuff.id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    string updateQuery = "UPDATE stuff SET name=@name, id_dep=@id_dep WHERE id=@id";
                    con = new NpgsqlConnection(ConnectionString);
                    con.Open();
                    con.Execute(updateQuery, stuff);
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
            return View(stuff);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM stuff WHERE id=@id";
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
                string searchQuery = "SELECT * FROM stuff WHERE id=@id";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                Stuff stuff = con.Query<Stuff>(searchQuery, new { id = id }).FirstOrDefault();
                return View(stuff);
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
