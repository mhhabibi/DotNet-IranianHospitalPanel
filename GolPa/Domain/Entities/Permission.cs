using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Title { get; set; }

        public enum Permisson
        {
            AccessToPerformingCourses = 3,
            AccessToFinishedCourses = 4,
            AccessToCreateQuiz = 5,
            AccessToCreatePersonQuiz = 6,
            AccessToReadEducationalCalender = 7,
            AccessToTesting = 8,
            AccessToRequestCourse = 9,
            AccessToFinishedCoursesAsPersonel = 10,
            AccessToFinishedCoursesAsSupervisor = 11,
            AccessToFinishedCoursesAsProfessor = 12,
            AccessToFinishedCoursesAsAdmin = 13,
            AccessToPerformingCoursesAsPersonel = 14,
            AccessToPerformingCoursesAsSupervisor = 15,
            AccessToPerformingCoursesAsProfessor = 16,
            AccessToPerformingCoursesAsAdmin = 17,
            AccessToNotConfirmedCourses = 18,
            AccessToAcceptRequestCourses = 19,
            AccessToAddAttachFile = 20,
            AccessToGetAttachedFiles = 21,
            AccessToReadCourses = 22,
            AccessToAddCourse = 23,
            AccessToReadDepartmans = 24,
            AccessToAddDepartman = 25,
            AccessToReadExperts = 26,
            AccessToAddExpert = 27,
            AccessToReadGroups = 28,
            AccessToAddGroup = 29,
            AccessToReadIllnesses = 30,
            AccessToAddIllness = 31,
            AccessToReadJobs = 32,
            AccessToAddJob = 33,
            AccessToReadJobGroups = 34,
            AccessToAddJobGroup = 35,
            AccessToReadPatients = 36,
            AccessToAddPatient = 37,
            AccessToReadPersonels = 38,
            AccessToAddPersonel = 39,
            AccessToReadProfessors = 40,
            AccessToReadQuestions = 41,
            AccessToAddQuestion = 42,
            AccessToReadAnswers = 43,
            AccessToAddAnswer = 44,
            AccessToReadQuizzes = 45,
            AccessToAddQuiz = 46,
            AccessToCurrentUser=51,
            AccessToReadQuizzesAsAdmin=50,
            AccessToReadQuizzesAsProfessor=49,
            AccessToReadQuizzesAsSupervisor=48,
            AccessToReadQuizzesAsPersonel=47,
            AccessToReadDepartmanPersonel=52
        }
    }
}
