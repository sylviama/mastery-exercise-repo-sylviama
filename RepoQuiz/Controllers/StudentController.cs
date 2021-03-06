﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepoQuiz.DAL;
using RepoQuiz.Models;

namespace RepoQuiz.Controllers
{
    public class StudentController : Controller
    {
        public StudentRepository repo { get; set; }

        public StudentController()
        {
            repo= new StudentRepository();
        }

        public StudentController(StudentRepository repo1)
        {
            repo = repo1;
        }

        // GET: Student
        public ActionResult Index()
        {
            ViewBag.studentList = repo.GetAllStudents();
            return View();
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.oneStudent = repo.ReturnOneStudent(id);
            return View();
        }

        public ActionResult New()
        {
            ViewBag.generatedNewStudent = repo.SaveStudentToDb();
            return View();
        }

        public ActionResult Delete(int id)
        {
            repo.RemoveAStudent(id);
            return View();
        }

    }
}
