using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Provisioning.Service;

namespace deviceProvisioningServiceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSample().GetAwaiter().GetResult();
            Console.WriteLine("\nHit <Enter> to exit ...");
            Console.ReadLine();
        }

        private static string ProvisioningConnectionString = "HostName=my-dps.azure-devices-provisioning.net;SharedAccessKeyName=provisioningserviceowner;SharedAccessKey=WjLv43ZKrOPLYDOdprhqxA8nH2sHFtszX+5XKBPCPRM=";
        private const string RegistrationId = "sample-registrationid-csharp";
        private const string TpmEndorsementKey = "AToAAQALAAMAsgAgg3GXZ0SEs/gakMyNRqXXJP1S124GUgtk8qHaGzMUaaoABgCAAEMAEAgAAAAAAAEAppKDK/aVnk7ifMGGCH5YELmqWxBUQU/3Wlorj3QmU98cW7hdUzBWRijfoXPuyEnC1KyQMQ235EM+9zrCHYJqKRj6l9rRNfKZBD4ox4qS6ViD9V0f7g41bdYhYHUHkRJtXsrlAWL7sYZocNpBNXmmkzlQD9oIVGfB46GyGt2gH77O4VH8cqv4wjscvXJob/NNpoUSuKrh8zCWhF/AFsGe+u2/zE2kylqCcPskSL7mxkMVjEdsxeiMcvdpEg/GKdom64l31POqy4skAUZ87fPhMurYroMc20F3G6Xqpg1IJVBKvjPRBHrcZHmltGDkcua9GfDEKe0EPIXEI+8uVV8SHw==";

        public static async Task RunSample()
        {
            Console.WriteLine("Starting sample...");

            using (ProvisioningServiceClient provisioningServiceClient =
                    ProvisioningServiceClient.CreateFromConnectionString(ProvisioningConnectionString))
            {
                #region Create a new individualEnrollment config
                Console.WriteLine("\nCreating a new individualEnrollment...");
                Attestation attestation = new TpmAttestation(TpmEndorsementKey);
                IndividualEnrollment individualEnrollment =
                        new IndividualEnrollment(
                                RegistrationId,
                                attestation);
                #endregion

                #region Create the individualEnrollment
                Console.WriteLine("\nAdding new individualEnrollment...");
                IndividualEnrollment individualEnrollmentResult =
                    await provisioningServiceClient.CreateOrUpdateIndividualEnrollmentAsync(individualEnrollment).ConfigureAwait(false);
                Console.WriteLine("\nIndividualEnrollment created with success.");
                Console.WriteLine(individualEnrollmentResult);
                #endregion

            }
        }
    }
}
