using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

/*
 * Sample from stackoverflow question: http://stackoverflow.com/questions/39423446/c-sharp-unit-test-fails-with-moq-when-it-goes-to-hit-a-real-service
 * 
 * 
 *
 *
 * */

namespace SampleKeyDocumentService
{
    [TestFixture]
    public class KeyDocumentServiceTests
    {
        [Test]
        public void KeyDocumentService_ProofKeyDocument_RepoReturnsData_ServiceReturnsTheDataWithoutError()
        {
            //Arrange
            KeyDocumentProofRequest request = new KeyDocumentProofRequest() { KeyDocumentId = 2 };
            string returnedResponse = "2";
            KeyDocument keyDocumentResponse = new KeyDocument() { CampaignId = "2", DesignFileId = 3, DocumentId = "2", DataSourceId = "3", KeyDocumentId = 1 };
            List<vwKeyDocumentSearch> keyListResponse = new List<vwKeyDocumentSearch>() { new vwKeyDocumentSearch { FieldName = "test", FieldValue = "testvalue" } };
            var uproduceRepo = new Mock<IUProduceRepository>();
            var keyDocRepo = new Mock<IKeyDocumentRepository>();
            var templateRepo = new Mock<ITemplateRepository>();
            keyDocRepo.Setup(p => p.GetKeyDocument(It.IsAny<KeyDocumentRequest>())).Returns(new KeyDocumentResponse() { data = keyDocumentResponse });
            keyDocRepo.Setup(p => p.GetKeyDocumentItems(It.IsAny<int>())).Returns(keyListResponse);
            uproduceRepo.Setup(p => p.ProduceDocument(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Customization[]>(), It.IsAny<string>(), It.IsAny<string>(), null)).Returns(returnedResponse);
            // Act.
            KeyDocumentService svc = new KeyDocumentService(keyDocRepo.Object, uproduceRepo.Object, templateRepo.Object);
            var response = svc.ProofKeyDocument(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.data.JobId);
            Assert.IsNull(response.Error);
        }
    }

    public interface IUProduceRepository
    {
        string ProduceDocument(string isAny, string s, Customization[] customizations, string isAny1, string s1, object o);
        string CreateJobTicket(string documentId, string dataSourceId, string proof);
    }
    public interface IKeyDocumentRepository
    {
        KeyDocumentResponse GetKeyDocument(KeyDocumentRequest request);
        List<vwKeyDocumentSearch> GetKeyDocumentItems(int documentKey);
    }
    public interface ITemplateRepository
    {
        
    }

    public class JobDataSource
    {
        public object JobId;
    }

    public class KeyDocument
    {
        public string CampaignId { get; set; }
        public int? DesignFileId { get; set; }
        public string DocumentId { get; set; }
        public string DataSourceId { get; set; }
        public int KeyDocumentId { get; set; }
    }

    public class vwKeyDocumentSearch
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Type { get; set; }
    }

    public class KeyDocumentResponse
    {
        public KeyDocument data { get; set; }
    }
    public class KeyDocumentProofRequest
    {
        public int KeyDocumentId { get; set; }
    }

    public class KeyDocumentRequest
    {
        public int KeyDocumentId { get; set; }
    }
    public class KeyDocumentProofResponse
    {
        public JobDataSource data;
        public CustomError Error { get; set; }
    }

    public class CustomError
    {
        
    }

    public class Customization
    {
        
    }
    public class KeyDocumentService
    {
        private readonly IKeyDocumentRepository _repo;
        private readonly IUProduceRepository _uproduceRepo;
        private ITemplateRepository _templateRepoObject;
        public KeyDocumentService(IKeyDocumentRepository keyDocumentRepository, IUProduceRepository uproduceRepoObject, ITemplateRepository templateRepoObject)
        {
            _repo = keyDocumentRepository;
            _uproduceRepo = uproduceRepoObject;
            _templateRepoObject = templateRepoObject;
        }

        public KeyDocumentProofResponse ProofKeyDocument(KeyDocumentProofRequest request)
        {
            //return new KeyDocumentProofResponse()
            //{
            //    data = new KeyDocumentProofResponseData() { JobId = "2984" }
            //};
            KeyDocumentProofResponse response = new KeyDocumentProofResponse();
            var keyDocumentDetails = _repo.GetKeyDocument(new KeyDocumentRequest() { KeyDocumentId = request.KeyDocumentId }).data;
            if (keyDocumentDetails != null && (!string.IsNullOrEmpty(keyDocumentDetails.CampaignId)) &&
                    keyDocumentDetails.DesignFileId.HasValue &&
                    keyDocumentDetails.DesignFileId > 0)
            {

                var keyDocumentResponse = _repo.GetKeyDocument(new KeyDocumentRequest() { KeyDocumentId = request.KeyDocumentId });

                Customization[] customizations = GenerateCustomizationsForKeyDocument(keyDocumentDetails.KeyDocumentId, keyDocumentResponse);

                var jobTicketId = _uproduceRepo.CreateJobTicket(keyDocumentDetails.DocumentId, keyDocumentDetails.DataSourceId, "PROOF");
                if (!string.IsNullOrEmpty(jobTicketId))
                {
                    List<JobDataSource> dataSources = GenerateCSVForPersonalizedAndCustomizedVariables(keyDocumentResponse, jobTicketId);

                    var jobId = _uproduceRepo.ProduceDocument(keyDocumentDetails.DocumentId, keyDocumentDetails.DataSourceId, customizations, "PROOF", jobTicketId, dataSources);

                    if (string.IsNullOrEmpty(jobId))
                    {
                        response.Error = CreateCustomError("Error while submitting job", "Error occurred while submitting proofing job");
                    }
                    else
                    {
                        response.data.JobId = jobId;
                    }
                }
                else
                {
                    response.Error = CreateCustomError("Unable to generate job ticket for the keydocument",
                    "Error while creating a job ticket for proof request");
                }
            }
            else
            {
                response.Error = CreateCustomError("Unable to generate proof for the keydocument",
                    "Requested template is missing campaignid or Designfile in Uproduce");
            }

            return response;
        }

        private CustomError CreateCustomError(string errorWhileSubmittingJob, string errorOccurredWhileSubmittingProofingJob)
        {
            return new CustomError();
        }

        private List<JobDataSource> GenerateCSVForPersonalizedAndCustomizedVariables(KeyDocumentResponse keyDocumentResponse, string jobTicketId)
        {
            return new List<JobDataSource>();
        }

        private Customization[] GenerateCustomizationsForKeyDocument(int keyDocumentId, KeyDocumentResponse keyDocumentResponse)
        {
            return new Customization[]{};
        }

        private List<Customization> GetCustomizationsFromKeyDocumentItems(List<vwKeyDocumentSearch> keyDocumentItemsList,
                                       int templateId, int clientId)
        {
            try
            {
                List<Customization> KeyDocumentCustomizations = new List<Customization>();

                var keyDocumentVariableList = keyDocumentItemsList.Where(k => k.Type.ToUpper() == "VARIABLE").ToList();
                var keyDocumentSettingList = keyDocumentItemsList.Where(k => k.Type.ToUpper() == "SETTING").ToList();
                var keyDocumentContentList = keyDocumentItemsList.Where(k => k.Type.ToUpper() == "CONTENT").ToList();

                KeyDocumentCustomizations.AddRange(VariableCustomizations(keyDocumentVariableList, templateId));
                KeyDocumentCustomizations.AddRange(SettingCustomizations(keyDocumentSettingList, templateId));
                KeyDocumentCustomizations.AddRange(ContentCustomizations(keyDocumentContentList, templateId, clientId));

                return KeyDocumentCustomizations;
            }
            catch (Exception ex)
            {
                //logger.Error(string.Format("Error customizing key document: {0}", templateId), ex);
                throw ex;
            }
        }

        private IEnumerable<Customization> ContentCustomizations(List<vwKeyDocumentSearch> keyDocumentContentList, int templateId, int clientId)
        {
            return new List<Customization>();
        }

        private IEnumerable<Customization> SettingCustomizations(List<vwKeyDocumentSearch> keyDocumentSettingList, int templateId)
        {
            return new List<Customization>();
        }

        private IEnumerable<Customization> VariableCustomizations(List<vwKeyDocumentSearch> keyDocumentVariableList, int templateId)
        {
            return new List<Customization>();
        }
    }
}
