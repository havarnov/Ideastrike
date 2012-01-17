using System.Linq;
using Ideastrike.Nancy.Models;
using Nancy;

namespace Ideastrike.Nancy.Modules
{
    public class IdeaModule : NancyModule
    {
        public IdeaModule()
        {
            Get["/idea/{id}"] = parameters =>
            {
                using (var db = new IdeastrikeContext())
                {
                    double id = parameters.id;
                    var idea = db.Ideas.FirstOrDefault(i => i.Id == id);
                    return string.Format("Id:{0} Title:{1} Description:{2}", idea.Id, idea.Title, idea.Description);
                }
            };

            Get["/idea/{id}/delete"] = parameters =>
            {
                using (var db = new IdeastrikeContext())
                {
                    double id = parameters.id;
                    var idea = db.Ideas.FirstOrDefault(i => i.Id == id);
                    db.Ideas.Remove(idea);
                    db.SaveChanges();
                    return string.Format("Deleted Item {0}", id);
                }
            };

                        Post["/idea/add"] = parameters =>
            {
                using (var db = new IdeastrikeContext())
                {
                    var author = Request.Form.author;
                    var title = Request.Form.title;
                    var desc = Request.Form.description;
                   
                    var newIdea = db.Ideas.Add(new Idea {Title = title, Author = author, Description = desc });
                    if(newIdea.isValid())
                    {
                        // store the new idea and redirect to the new idea
                        db.SaveChanges();
                        return Response.AsRedirect(string.Format("/idea/{0}/", newIdea.Id));
                    }
                    else
                    {
                        // return to form with errors
                        List<string> errorList = newIdea.generateErrorList();
                        return View["Home/Add", new { errorList, author, title, desc }];
                    }
                }
            };
        }
    }
}
