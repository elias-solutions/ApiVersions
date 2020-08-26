using System.Collections.Generic;
using System.Linq;
using Api.ErrorHandling;
using Api.Models.V1;

namespace Api.Repository
{
    public class SampleRepository
    {
        // Not threadsafe, also not a dictionary...
        private List<Sample> _samples => GetAll().ToList();

        public IEnumerable<Sample> GetAll()
        {
            yield return new Sample {Id = 0, Name = "TestSample", Value = 100};
            yield return new Sample {Id = 1, Name = "Another Test Sample", Value = 100};
            yield return new Sample {Id = 2, Name = "Cool", Value = 100};
        }

        public Sample GetById(long id)
        {
            var sample = _samples.FirstOrDefault(s => s.Id == id);

            if (sample != null)
            {
                return sample;
            }

            throw new ProblemDetailsException(404, "Could not find resource", $"Could not find sample with Id '{id}'");
        }

        public void DeleteById(long id)
        {
            var sample = _samples.Find(item => item.Id == id);
            if (sample == null)
            {
                throw new ProblemDetailsException(404, "Could not find resource", $"Could not find sample with Id '{id}'");
            }

            _samples.Remove(sample);
        }

        public Sample Create(Sample sample)
        {
            // This is fixed to force an exception after posting twice
            const long id = 6;

            if (_samples.Any(s => s.Id == id))
            {
                throw new ProblemDetailsException(400, "Resource already exists", $"The sample with Id '{id}' already exists. Cannot create a new one.");
            }
                
            var newSample = new Sample
            {
                Id = id,
                Name = sample.Name,
                Value = sample.Value
            };

            _samples.Add(newSample);

            return newSample;
        }

        public void Replace(long id, Sample sampleToReplace)
        {
            var sample = _samples.Find(item => item.Id == id);
            if (sample == null)
            {
                throw new ProblemDetailsException(404, "Could not find resource", $"Could not find sample with Id '{id}'");
            }

            _samples.Remove(sample);
            _samples.Add(sampleToReplace);
        }

        public void Update(long id, Sample sampleToUpdate)
        {
            var sample = _samples.Find(item => item.Id == id);
            if (sample == null)
            {
                throw new ProblemDetailsException(404, "Could not find resource", $"Could not find sample with Id '{id}'");
            }

            sample.Name = sampleToUpdate.Name;
            sample.Value = sampleToUpdate.Value;
        }
    }
}