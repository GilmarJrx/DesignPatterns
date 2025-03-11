using System.Collections;
using System.Collections.ObjectModel;
using static DesignPatterns.Composite.Composite;

namespace DesignPatterns.Composite
{
    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach (var from in self)
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
        }
    }

    public class Composite
    {
        // we cannot use a base class 

        public class Neuron : IEnumerable<Neuron>
        {
            public float Value;
            public required List<Neuron> In, Out;

            public IEnumerator<Neuron> GetEnumerator()
            {
                yield return this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                yield return this;
            }
        }

        public class NeuronLayer : Collection<Neuron>
        {

        }

        public static void RunDemo()
        {
            var neuron1 = new Neuron()
            { In = new List<Neuron>(), Out = new List<Neuron>() };
            var neuron2 = new Neuron()
            { In = new List<Neuron>(), Out = new List<Neuron>() };

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            neuron1.ConnectTo(neuron2);
            neuron1.ConnectTo(layer1);
            layer1.ConnectTo(layer2);
        }
    }
}