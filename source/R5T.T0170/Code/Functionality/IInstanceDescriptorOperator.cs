using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;
using R5T.T0172;


namespace R5T.T0170
{
    [FunctionalityMarker]
    public partial interface IInstanceDescriptorOperator : IFunctionalityMarker
    {
        /// <summary>
        /// <inheritdoc cref="Documentation.For_CompareRunInstances" path="/summary"/>
        /// </summary>
        /// <param name="runInstances">The instances from today's run.</param>
        /// <param name="priorToTodayInstances">The full set of instances accumulated over time.</param>
        /// <param name="projectsToIgnoreFilePathsHash">The list of projects that failed to build or to process during today's run.</param>
        public (
            InstanceDescriptor[] newInstances,
            InstanceDescriptor[] removedInstances,
            InstanceDescriptor[] missingInstances)
        CompareRunInstances(
            IList<InstanceDescriptor> runInstances,
            IList<InstanceDescriptor> priorToTodayInstances,
            // Do not include "do not build" projects, so that any instances in them are advertised forever.
            HashSet<IProjectFilePath> projectsToIgnoreFilePathsHash)
        {
            // Use an equality comparer that does not care about the description (since the description being updated doesn't really create a new instance descriptor).
            //var instanceDescriptorEqualityComparer = N002.InstanceDescriptorEqualityComparer.Instance;
            // UH-OH! Because later code uses only the new and removed instances for updating the instances file, we need to include description in new and removed testing.
            var instanceDescriptorEqualityComparer = N001.InstanceDescriptorEqualityComparer.Instance;

            // Determine added instances: these are easy, it's just what instances exist in the "per-run" file that don't exist in the "prior-to" today file.
            var firstRunInstances = runInstances.TakeFirst();
            var firstPriorInstances = priorToTodayInstances.TakeFirst();

            var newInstances = runInstances.Except(
                priorToTodayInstances,
                instanceDescriptorEqualityComparer)
                .Now();

            // Determine removed instances: these are harder.
            // First determine what instances exist in the "prior-to" today file that do not exist in the "per-run" file.
            // Then load the build problems and processing problems file paths.
            // For any instances that are in projects in either of the build problems or processing problems files, remove them from the list.
            // Whatever instances remain, those instances have actually been removed.
            var missingInstances = priorToTodayInstances.Except(
                runInstances,
                instanceDescriptorEqualityComparer)
                .Now();

            var removedInstances = missingInstances
                .Where(instance =>
                {
                    var ignoreProject = projectsToIgnoreFilePathsHash.Contains(
                        instance.ProjectFilePath);

                    var output = !ignoreProject;
                    return output;
                })
                .Now();

            return (newInstances, removedInstances, missingInstances);
        }

        /// <summary>
        /// Fully evaluate all properties of an instance descriptor.
        /// </summary>
        public bool Equals_Full(InstanceDescriptor x, InstanceDescriptor y)
        {
            // Test full equality to determine if two instances really are exactly the same, but order the property equality according to discriminatory power
            // to take advantage of the short-circuiting operator-&& in C#. This speeds performance since if one of the equality tests is false, the rest are not evaluated.
            var output = true
                // Put identity name first since identity name will get you 99% of the way to uniquely identifying a instance descriptor.
                && x.IdentityString.Equals(y.IdentityString)
                // Put the kind-marked full member name second since it gets you 99.9% of the way.
                && x.SignatureString.Equals(y.SignatureString)
                // Put obsolete third, since it's a boolean and thus easy to evaluate.
                && x.IsObsolete.Equals(y.IsObsolete)
                // Even though the kind-marked part of the kind-marked full member name gets you most of the way to the variety, there are still multiple varieties that are all methods.
                && x.InstanceVarietyName.Equals(y.InstanceVarietyName)
                // Second to last is the project, since the identity name contains the namespace, which is basically the project.
                && x.ProjectFilePath.Equals(y.ProjectFilePath)
                // Finally, it might be basically the same instance, just with an update to the description.
                && x.DescriptionXml.Equals(y.DescriptionXml)
                ;

            return output;
        }

        /// <summary>
        /// Evaluate all properties except for description, since we might not care about description.
        /// </summary>
        public bool Equals_WithoutDescription(InstanceDescriptor x, InstanceDescriptor y)
        {
            //var identityNameEqual = x.IdentityName == y.IdentityName;
            //var kindMarkedFullMemberNameEqual = x.KindMarkedFullMemberName == y.KindMarkedFullMemberName;
            //var isObsoleteEqual = x.IsObsolete == y.IsObsolete;
            //var instanceVarietyNameEqual = x.InstanceVarietyName == y.InstanceVarietyName;
            //// Need to use Equals(), not == operator.
            //var projectFilePathEqual = x.ProjectFilePath.Equals(y.ProjectFilePath);

            //var output = true
            //    && identityNameEqual
            //    && kindMarkedFullMemberNameEqual
            //    && isObsoleteEqual
            //    && instanceVarietyNameEqual
            //    && projectFilePathEqual
            //    ;

            //return output;

            /// See ordering notes in <see cref="Equals_Full(InstanceDescriptor, InstanceDescriptor)"/>.
            var output = true
                && x.IdentityString.Equals(y.IdentityString)
                && x.SignatureString.Equals(y.SignatureString)
                && x.IsObsolete.Equals(y.IsObsolete)
                && x.InstanceVarietyName.Equals(y.InstanceVarietyName)
                && x.ProjectFilePath.Equals(y.ProjectFilePath)
                ;

            return output;
        }

        public int Get_HashCode(InstanceDescriptor obj)
        {
            // Only need to use the project file path and the identity name since those will uniquely identify anything in C#.
            var hashCode = HashCode.Combine(
                obj.ProjectFilePath,
                // Use identity name, not kind-marked full member name, since identity name will be unique in the project and all the extra parameter name strings and stuff are just extra work.
                obj.IdentityString);

            return hashCode;
        }
    }
}
