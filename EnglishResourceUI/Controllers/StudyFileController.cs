using EnglishResourceUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishResourceUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StudyFileController : Controller
    {
        private readonly IStudyFileRepository _fileRepo;
        private readonly ITopicRepository _topicRepo;
        private readonly ILevelRepository _levelRepo;

        public StudyFileController(IStudyFileRepository fileRepo, ITopicRepository topicRepo, ILevelRepository levelRepo)
        {
            _fileRepo = fileRepo;
            _topicRepo = topicRepo;
            _levelRepo = levelRepo;
        }

        public async Task<IActionResult> Index()
        {
            var studyFiles = await _fileRepo.GetFiles();
            return View(studyFiles);
        }

        public async Task<IActionResult> AddFile()
        {
            var topicSelectList = (await _topicRepo.GetTopics()).Select(topic => new SelectListItem
            {
                Text = topic.TopicName,
                Value =topic.Id.ToString()
            });
            var levelSelectList = (await _levelRepo.GetLevels()).Select(level => new SelectListItem
            {
                Text = level.CEFRLevel,
                Value = level.Id.ToString()
            });
            StudyFileDTO fileToAdd = new() {TopicList = topicSelectList, LevelList = levelSelectList};
            return View(fileToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(StudyFileDTO fileToAdd)
        {
            var topicSelectList = (await _topicRepo.GetTopics()).Select(topic => new SelectListItem
            {
                Text = topic.TopicName,
                Value = topic.Id.ToString()
            });
            fileToAdd.TopicList = topicSelectList;
            var levelSelectList = (await _levelRepo.GetLevels()).Select(level => new SelectListItem
            {
                Text = level.CEFRLevel,
                Value = level.Id.ToString()
            });
            fileToAdd.LevelList = levelSelectList;

            if (!ModelState.IsValid)
                return View(fileToAdd);

            try
            {
                // manual mapping of BookDTO -> Book
                StudyFile studyFile = new()
                {
                    Id = fileToAdd.Id,
                    FileName = fileToAdd.FileName,
                    FileURL = fileToAdd.FileURL,
                    TopicId = fileToAdd.TopicId,
                    LevelId = fileToAdd.LevelId,
                };
                await _fileRepo.AddFile(studyFile);
                TempData["successMessage"] = "File is added successfully";
                return RedirectToAction(nameof(AddFile));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(fileToAdd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(fileToAdd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(fileToAdd);
            }
        }

        public async Task<IActionResult> UpdateFile(int id)
        {
            var studyFile = await _fileRepo.GetFileById(id);
            if (studyFile == null)
            {
                TempData["errorMessage"] = $"File with the id: {id} was not found";
                return RedirectToAction(nameof(Index));
            }
            var topicSelectList = (await _topicRepo.GetTopics()).Select(topic => new SelectListItem
            {
                Text = topic.TopicName,
                Value = topic.Id.ToString(),
                Selected = topic.Id == studyFile.TopicId
            });
            var levelSelectList = (await _levelRepo.GetLevels()).Select(level => new SelectListItem
            {
                Text = level.CEFRLevel,
                Value = level.Id.ToString(),
                Selected = level.Id == studyFile.TopicId
            });


            StudyFileDTO fileToUpdate = new()
            {
                TopicList = topicSelectList,
                LevelList = levelSelectList,
                FileName = studyFile.FileName,
                FileURL = studyFile.FileURL,
                TopicId = studyFile.TopicId,
                LevelId = studyFile.LevelId,
            };
            return View(fileToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFile(StudyFileDTO fileToUpdate)
        {
            var topicSelectList = (await _topicRepo.GetTopics()).Select(topic => new SelectListItem
            {
                Text = topic.TopicName,
                Value = topic.Id.ToString(),
                Selected = topic.Id == fileToUpdate.TopicId
            });
            var levelSelectList = (await _levelRepo.GetLevels()).Select(level => new SelectListItem
            {
                Text = level.CEFRLevel,
                Value = level.Id.ToString(),
                Selected = level.Id == fileToUpdate.TopicId
            });
            fileToUpdate.TopicList = topicSelectList;
            fileToUpdate.LevelList = levelSelectList;

            if (!ModelState.IsValid)
                return View(fileToUpdate);

            try
            {
                // manual mapping of BookDTO -> Book
                StudyFile studyFile = new()
                {
                    Id = fileToUpdate.Id,
                    FileName = fileToUpdate.FileName,
                    FileURL = fileToUpdate.FileURL,
                    TopicId = fileToUpdate.TopicId,
                    LevelId = fileToUpdate.LevelId,
                };
                await _fileRepo.UpdateFile(studyFile);
                TempData["successMessage"] = "File is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(fileToUpdate);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(fileToUpdate);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(fileToUpdate);
            }
        }

        public async Task<IActionResult> Deletefile(int id)
        {
            try
            {
                var studyFile = await _fileRepo.GetFileById(id);
                if (studyFile == null)
                {
                    TempData["errorMessage"] = $"File with the id: {id} was not found";
                }
                else
                {
                    await _fileRepo.DeleteFile(studyFile);
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on deleting the data";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
