using System.Text;

namespace JSonPortingTest.Scenario
{
    public class CreateJsonScenario
    {
        private StringBuilder _jsonStringBuilder = new StringBuilder();

        public CreateJsonScenario WithRoot(string key, string value)
        {
            _jsonStringBuilder.Append($"\"{key}\": \"{value}\",");
            return this;
        }

        public CreateJsonScenario WithRoot(string key)
        {
            _jsonStringBuilder.Append($"\"{key}\": {{");
            return this;
        }

        public CreateJsonScenario WithChildren(string key, Action<CreateJsonScenario> configureChildren)
        {
            _jsonStringBuilder.Append($"\"{key}\": {{");
            configureChildren(this);
            _jsonStringBuilder.Append("},");
            return this;
        }

        public string Create()
        {
            char lastChar = _jsonStringBuilder[_jsonStringBuilder.Length - 1];
            if (lastChar == ',')
                _jsonStringBuilder.Length--;

            _jsonStringBuilder = _jsonStringBuilder.Replace(",}", "}");

            var totalOpenBrachet = _jsonStringBuilder.ToString().Count(x => x == '{');
            var totalCloseBrachet = _jsonStringBuilder.ToString().Count(x => x == '}');
            var diff = totalOpenBrachet - totalCloseBrachet;
            if (diff > 0)
                for (var i = 0; i < diff; i++)
                    _jsonStringBuilder.Append('}');

            return "{" + _jsonStringBuilder.ToString() + "}";
        }
    }
}
