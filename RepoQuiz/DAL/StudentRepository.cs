﻿using RepoQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepoQuiz.DAL
{
    public class StudentRepository
    {
        public StudentContext Context { get; set; }

        public StudentRepository()
        {
            Context = new StudentContext();
        }

        
        public StudentRepository(StudentContext studentContext)
        {
            Context = studentContext;
        }


        //return four database tables
        public virtual List<FirstNamePick> FirstNamePickList()
        {
            return Context.firstNamePick.ToList();
        }

        public virtual List<LastNamePick> LastNamePickList()
        {
            return Context.lastNamePick.ToList();
        }

        public virtual List<MajorPick> MajorPickList()
        {
            return Context.majorPick.ToList();
        }

        public virtual List<Student> GetAllStudents()
        {
            return Context.Students.ToList();
        }

        public virtual Student ReturnOneStudent(int id)
        {
            var student = Context.Students.FirstOrDefault(s => s.StudentID == id);
            return student;
        }

        //delete a record
        public virtual void RemoveAStudent(int id)
        {
            var student= Context.Students.FirstOrDefault(s => s.StudentID == id);
            Context.Students.Remove(student);
            Context.SaveChanges();
        }


        //test if record is duplicate
        public virtual bool TestIfDuplicate(Student newStudent)
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
        public virtual Student SaveStudentToDb()
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
                Student student = new Student() { FirstName=""};
                return student;
            } 
        }
    }
}