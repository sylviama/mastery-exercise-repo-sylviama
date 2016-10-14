﻿using RepoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepoQuiz.DAL
{
    public class StudentRepository
    {
        StudentContext Context { get; set; }

        public StudentRepository()
        {
            Context = new StudentContext();
        }


        //return four database tables
        public StudentRepository(StudentContext studentContext)
        {
            Context = studentContext;
        }

        public List<FirstNamePick> FirstNamePickList()
        {
            return Context.firstNamePick.ToList();
        }

        public List<LastNamePick> LastNamePickList()
        {
            return Context.lastNamePick.ToList();
        }

        public List<MajorPick> MajorPickList()
        {
            return Context.majorPick.ToList();
        }

        public List<Student> GetAllStudents()
        {
            return Context.Students.ToList();
        }

        public Student ReturnOneStudent(int id)
        {
            var student = Context.Students.FirstOrDefault(s => s.StudentID == id);
            return student;

        }


        //test if record is duplicate
        public bool TestIfDuplicate(Student newStudent)
        {
            int counter = 0;

            var list = GetAllStudents();
            foreach (var item in list)
            {
                if (item.FirstName + item.LastName == newStudent.FirstName + newStudent.LastName)
                {
                    counter++;
                }
            }

            if (counter == 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        //save to database if not duplicate
        public Student SaveStudentToDb()
        {
            NameGenerator record = new NameGenerator();
            var newStudent = record.GenerateRamdomStudentCombination();
            if(TestIfDuplicate(newStudent)==true)
            {
                Context.Students.Add(newStudent);
                Context.SaveChanges();
                return newStudent;
            }else
            {
                Student student = new Student();
                return student;
            } 
        }
    }
}