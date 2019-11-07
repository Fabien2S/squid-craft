using System;
using System.Collections.Generic;
using System.Globalization;
using SquidCraft.API.Utils;

namespace SquidCraft.API.Math
{
    [Serializable]
    public class MagicalFloat
    {
        public enum Operation
        {
            Add,
            AddScalar
        }

        public event EventHandler<float> ValueChanged;

        public float BaseValue
        {
            get => _value;
            set
            {
                this._value = value;
                Invalidate();
            }
        }

        private readonly Dictionary<Identifier, Modifier> _modifiers = new Dictionary<Identifier, Modifier>();

        private float _value;
        [NonSerialized] private float _cachedValue;

        public MagicalFloat(float value)
        {
            _value = value;
            _cachedValue = value;
        }

        private void Invalidate()
        {
            _cachedValue = float.NaN;
            if (ValueChanged != null)
                RecomputeValue();
        }

        private float RecomputeValue()
        {
            _cachedValue = _value;
            foreach (var modifier in _modifiers.Values)
            {
                _cachedValue += modifier.Operation switch
                {
                    Operation.Add => modifier.Value,
                    Operation.AddScalar => (_cachedValue * modifier.Value),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            ValueChanged?.Invoke(this, _cachedValue);
            return _cachedValue;
        }

        public override string ToString()
        {
            return ((float) this).ToString(CultureInfo.InvariantCulture);
        }

        public Modifier? this[Identifier identifier]
        {
            get => _modifiers[identifier];
            set
            {
                if (value.HasValue)
                {
                    _modifiers[identifier] = value.Value;
                    Invalidate();
                }
                else if (_modifiers.Remove(identifier))
                    Invalidate();
            }
        }

        public static implicit operator float(MagicalFloat f)
        {
            return float.IsNaN(f._cachedValue) ? f.RecomputeValue() : f._cachedValue;
        }

        public struct Modifier
        {
            public readonly float Value;
            public readonly Operation Operation;

            public Modifier(float value, Operation operation) : this()
            {
                Value = value;
                Operation = operation;
            }
        }
    }
}