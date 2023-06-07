using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using WebGrease.Activities;
using static System.Collections.Specialized.BitVector32;

namespace ProjectAPI.Controllers
{
    public class ServerController : ApiController
    {
        private readonly DCAEntities db = new DCAEntities();
        [HttpGet]
        public HttpResponseMessage Login(string email, string password)
        {
            try
            {
                var user = db.Users.Where(s => s.username == email && s.password == password).FirstOrDefault();
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "false");
                }
                else
                {
                    var result = from u in db.Users
                                 where u.username == email && u.password == password
                                 select new {
                                     u.u_id,
                                     u.name,
                                     u.username,
                                     u.usertype,
                                     u.password,
                                     u.image,
                                 };

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetMembers(string usertype)
        {
            try
            {
                var user = db.Users.Where(s => s.usertype == usertype);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "false");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage NewCase()
        {
            try
            {
                {
                    HttpRequest request = HttpContext.Current.Request;

                    // Create a new Case object
                    Case newCase = new Case
                    {
                        rb_id = int.Parse(request["rb_id"]),
                        st_id = int.Parse(request["st_id"]),
                        datetime = DateTime.Now,
                        description = request["description"],
                        image = request["image"],
                        viol_date = DateTime.Parse(request["violation_date"]),
                        com_id = int.Parse(request["com_id"])
                    };
                    // Add the new Case to the Cases table
                    db.Cases.Add(newCase);
                    // Save changes to the database
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "New case created successfully!");
                }
            }
            catch (Exception ex)
            {
                // Get the inner exception message for details
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage SetMeeting()
        {
            try
            {
                {
                    HttpRequest request = HttpContext.Current.Request;
                    // Create a new Case object
                    Meeting newmeeting = new Meeting
                    {
                        c_id = int.Parse(request["c_id"]),
                        vn_id = int.Parse(request["vn_id"]),
                        meetingtime = DateTime.Parse(request["meetingtime"]),
                        //meetingtime = DateTime.Now,
                    };
                    // Add the new Case to the Cases table
                    db.Meetings.Add(newmeeting);
                    // Save changes to the database
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "New meeting created successfully!");
                }
            }
            catch (Exception ex)
            {
                // Get the inner exception message for details
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage Getcommetiee()
        {
            try
            {

                var result = db.Committees;

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetCases(bool ispunished)
        {
            try
            {
                if (ispunished)
                {
                    var user = db.Cases.Where(s => s.p_id != null);
                    if (user == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "false");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }
                }
                else
                {
                    var user = db.Cases.Where(s => s.p_id == null);
                    if (user == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "false");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetPunishment()
        {
            try
            {

                var result = db.Punishments;

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage SetPunishment()
        {
            try
            {
                {
                    HttpRequest request = HttpContext.Current.Request;
                    // Create a new Case object
                    AssignPunishment newpunish = new AssignPunishment
                    {
                        c_id = int.Parse(request["c_id"]),
                        p_id = int.Parse(request["p_id"]),
                    };
                    // Add the new Case to the Cases table
                    db.AssignPunishments.Add(newpunish);
                    // Save changes to the database
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "New Punishment created successfully!");
                }
            }
            catch (Exception ex)
            {
                // Get the inner exception message for details
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult AssignPunishment()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                int c_id = int.Parse(request["c_id"]);
                int f_type = int.Parse(request["f_type"]);
                List<int> pt_ids = request["pt_id"].Split(',').Select(int.Parse).ToList();
                DateTime startDate = DateTime.Parse(request["start_date"]);
                DateTime endDate = DateTime.Parse(request["end_date"]);
                int f_amount = int.Parse(request["f_amount"]);

               
                {
                    foreach (int pt_id in pt_ids)
                    {
                        // Create Punishment record
                        Punishment newPunishment = new Punishment
                        {
                            pt_id=pt_id,
                            f_type = f_type,
                            start_date = startDate,
                            end_date = endDate
                        };

                        if (f_type == 1 || f_type == 2)
                        {
                            newPunishment.f_amount = f_amount;
                        }

                        db.Punishments.Add(newPunishment);
                        db.SaveChanges();

                        int newPunishmentId = newPunishment.p_id;

                        // Assign Punishment to Case
                        AssignPunishment newAssignPunishment = new AssignPunishment
                        {
                            p_id = newPunishmentId,
                            c_id = c_id
                        };

                        db.AssignPunishments.Add(newAssignPunishment);
                        db.SaveChanges();
                    }

                    return Ok("Punishment record created successfully.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetReportedBy(string usertype)
        {
            try
            {

                var result = db.Users.Where(u=>u.usertype==usertype);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateuserImage()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpPostedFile imagefile = request.Files["image"];


                int id = int.Parse(request["u_id"]);
                string extension = imagefile.FileName.Split('.')[1];
                string filename = id + "." + extension;
                imagefile.SaveAs(HttpContext.Current.Server.
                               MapPath("~/Images/" + filename));
                User user = db.Users.Where(x => x.u_id == id).FirstOrDefault();
                user.image = filename;
                _ = db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Uploaded");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [HttpPost]
        public HttpResponseMessage UploadcaseImage()
        {
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpPostedFile imagefile = request.Files["image"];


                int id = int.Parse(request["u_id"]);
                int st_id = int.Parse(request["st_id"]);
                string extension = imagefile.FileName.Split('.')[1];
                // DateTime dt = DateTime.Now;
                string filename = st_id + "." + extension;
                // filename = filename + DateTime.Now.ToShortTimeString()+"."+extension;
                imagefile.SaveAs(HttpContext.Current.Server.
                               MapPath("~/Images/" + filename));
                Case cas = db.Cases.Where(x => x.st_id == id).FirstOrDefault();
                cas.image = filename;
                _ = db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Uploaded");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }






    }
}

