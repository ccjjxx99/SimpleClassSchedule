using SimpleClassSchedule.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleClassSchedule.Services
{
    class CourseDataStore : IDataStore<Lesson> {
        List<Lesson> Lessons;

        public CourseDataStore()
    {
        Lessons = new List<Lesson>();
        var mockLessons = new List<Lesson>
            {
                //new Lesson { Id = Guid.NewGuid().ToString(), Text = "First Lesson", Description="This is an Lesson description." },
                //new Lesson { Id = Guid.NewGuid().ToString(), Text = "Second Lesson", Description="This is an Lesson description." },
                //new Lesson { Id = Guid.NewGuid().ToString(), Text = "Third Lesson", Description="This is an Lesson description." },
                //new Lesson { Id = Guid.NewGuid().ToString(), Text = "Fourth Lesson", Description="This is an Lesson description." },
                //new Lesson { Id = Guid.NewGuid().ToString(), Text = "Fifth Lesson", Description="This is an Lesson description." },
                //new Lesson { Id = Guid.NewGuid().ToString(), Text = "Sixth Lesson", Description="This is an Lesson description." },
            };

        foreach (var Lesson in mockLessons)
        {
            Lessons.Add(Lesson);
        }
    }

    public async Task<bool> AddLessonAsync(Lesson Lesson)
    {
        Lessons.Add(Lesson);

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateLessonAsync(Lesson Lesson)
    {
        var oldLesson = Lessons.Where((Lesson arg) => arg.Id == Lesson.Id).FirstOrDefault();
        Lessons.Remove(oldLesson);
        Lessons.Add(Lesson);

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteLessonAsync(string id)
    {
        var oldLesson = Lessons.Where((Lesson arg) => arg.Id == id).FirstOrDefault();
        Lessons.Remove(oldLesson);

        return await Task.FromResult(true);
    }

    public async Task<Lesson> GetLessonAsync(string id)
    {
        return await Task.FromResult(Lessons.FirstOrDefault(s => s.Id == id));
    }

    public async Task<IEnumerable<Lesson>> GetLessonsAsync(bool forceRefresh = false)
    {
        return await Task.FromResult(Lessons);
    }
}
}
