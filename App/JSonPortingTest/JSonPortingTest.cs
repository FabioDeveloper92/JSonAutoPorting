using FluentAssertions;
using JsonPorting.JsonPorting;
using JSonPortingTest.Scenario;
using Xunit;

namespace JSonPortingTest
{
    public class JSonPortingTest
    {
        [Fact]
        public void get_the_same_file_with_all_keys_in_the_root()
        {
            var source = new CreateJsonScenario()
                .WithRoot("key1", "value1")
                .WithRoot("key2", "value2")
                .WithRoot("key3", "value3")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subkey1", "subvalue1");
                    child.WithRoot("subkey2", "subvalue2");
                })
                .Create();

            var target = new CreateJsonScenario()
               .WithRoot("key1", "value1")
               .WithRoot("key3", "value2")
               .WithRoot("key5", "value5")
               .WithChildren("child2", child =>
               {
                   child.WithRoot("subkey3", "subvalue3");
                   child.WithRoot("subkey4", "subvalue4");
               })
               .Create();

            var expectedJson = new CreateJsonScenario()
                .WithRoot("key1", "value1")
                .WithRoot("key2", "value2")
                .WithRoot("key3", "value2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subkey1", "subvalue1");
                    child.WithRoot("subkey2", "subvalue2");
                })
                .WithRoot("key5", "value5")
               .WithChildren("child2", child =>
               {
                   child.WithRoot("subkey3", "subvalue3");
                   child.WithRoot("subkey4", "subvalue4");
               })
                .Create();

            var item = new JsonPorting.JsonPorting.JsonPorting();
            var mergedStr = item.CopyMissingKeysAndValues(source, target);

            var isEqual = JsonFactory.CompareTwoJson(expectedJson, mergedStr);
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void get_the_same_file_with_all_keys_in_the_root_and_if_key_is_present_in_target_use_this_value()
        {
            var source = new CreateJsonScenario()
                .WithRoot("key1", "value1")
                .WithRoot("key2", "value2")
                .WithRoot("key3", "value3")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subkey1", "subvalue1");
                    child.WithRoot("subkey2", "subvalue2");
                })
                .Create();

            var target = new CreateJsonScenario()
               .WithRoot("key1", "targetValue1")
               .WithRoot("key3", "targetValue2")
               .WithRoot("key5", "value5")
               .WithChildren("child2", child =>
               {
                   child.WithRoot("subkey3", "subvalueTarget3");
                   child.WithRoot("subkey4", "subvalue4");
               })
               .Create();

            var expectedJson = new CreateJsonScenario()
                .WithRoot("key1", "targetValue1")
                .WithRoot("key2", "value2")
                .WithRoot("key3", "targetValue2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subkey1", "subvalue1");
                    child.WithRoot("subkey2", "subvalue2");
                })
                .WithRoot("key5", "value5")
               .WithChildren("child2", child =>
               {
                   child.WithRoot("subkey3", "subvalueTarget3");
                   child.WithRoot("subkey4", "subvalue4");
               })
                .Create();

            var item = new JsonPorting.JsonPorting.JsonPorting();
            var mergedStr = item.CopyMissingKeysAndValues(source, target);

            var isEqual = JsonFactory.CompareTwoJson(expectedJson, mergedStr);
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void get_the_same_file_with_all_keys_in_the_subroot()
        {
            var source = new CreateJsonScenario()
               .WithRoot("key1", "value1")
               .WithChildren("child1", child =>
               {
                   child.WithRoot("subkey1", "subvalue1");
                   child.WithRoot("subkey2", "subvalue2");
                   child.WithRoot("subkey3", "subvalue3");
               })
               .Create();

            var target = new CreateJsonScenario()
              .WithRoot("key1", "targetValue1")
               .WithChildren("child1", child =>
               {
                   child.WithRoot("subkey1", "subvalue1");
                   child.WithRoot("subkey4", "subvalue4");
               })
               .Create();

            var expectedJson = new CreateJsonScenario()
               .WithRoot("key1", "targetValue1")
               .WithChildren("child1", child =>
               {
                   child.WithRoot("subkey1", "subvalue1");
                   child.WithRoot("subkey2", "subvalue2");
                   child.WithRoot("subkey3", "subvalue3");
                   child.WithRoot("subkey4", "subvalue4");
               })
               .Create();


            var item = new JsonPorting.JsonPorting.JsonPorting();
            var mergedStr = item.CopyMissingKeysAndValues(source, target);

            var isEqual = JsonFactory.CompareTwoJson(expectedJson, mergedStr);
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void get_the_same_file_with_all_keys_in_the_subroot_and_if_key_is_present_in_subrootarget_use_this_value()
        {
            var source = new CreateJsonScenario()
               .WithRoot("key1", "value1")
               .WithChildren("child1", child =>
               {
                   child.WithRoot("subkey1", "subvalue1");
                   child.WithRoot("subkey2", "subvalue2");
                   child.WithRoot("subkey3", "subvalue3");
               })
               .Create();

            var target = new CreateJsonScenario()
              .WithRoot("key1", "value1")
               .WithChildren("child1", child =>
               {
                   child.WithRoot("subkey1", "subvalueTarget1");
                   child.WithRoot("subkey4", "subvalue4");
               })
               .Create();

            var expectedJson = new CreateJsonScenario()
               .WithRoot("key1", "value1")
               .WithChildren("child1", child =>
               {
                   child.WithRoot("subkey1", "subvalueTarget1");
                   child.WithRoot("subkey2", "subvalue2");
                   child.WithRoot("subkey3", "subvalue3");
                   child.WithRoot("subkey4", "subvalue4");
               })
               .Create();


            var item = new JsonPorting.JsonPorting.JsonPorting();
            var mergedStr = item.CopyMissingKeysAndValues(source, target);

            var isEqual = JsonFactory.CompareTwoJson(expectedJson, mergedStr);
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void get_the_same_file_with_all_keys_in_the_subsubroot()
        {
            // JSON di origine
            var source = new CreateJsonScenario()
            .WithRoot("key1", "value1")
            .WithRoot("key2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subchild1", "subchdvalue2");
                    child.WithRoot("subchild2", "subchdvalue3");
                })
                .WithChildren("child2", child =>
                {
                    child.WithRoot("subchild3", "subchdvalue4");
                    child.WithRoot("subchild4", "subchdvalue5");
                })
            .Create();

            // JSON di destinazione
            var target = new CreateJsonScenario()
            .WithRoot("key1", "value1")
            .WithRoot("key2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subchild1", "subchdvalue2");
                })
            .Create();

            string expectedJson = new CreateJsonScenario()
            .WithRoot("key1", "value1")
            .WithRoot("key2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subchild1", "subchdvalue2");
                    child.WithRoot("subchild2", "subchdvalue3");
                })
                .WithChildren("child2", child =>
                {
                    child.WithRoot("subchild3", "subchdvalue4");
                    child.WithRoot("subchild4", "subchdvalue5");
                })
            .Create();

            var item = new JsonPorting.JsonPorting.JsonPorting();
            var mergedStr = item.CopyMissingKeysAndValues(source, target);

            var isEqual = JsonFactory.CompareTwoJson(expectedJson, mergedStr);
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void get_the_same_file_with_all_keys_in_the_subsubroot_and_if_key_is_present_in_subsubrootarget_use_this_value()
        {
            // JSON di origine
            var source = new CreateJsonScenario()
            .WithRoot("key1", "value1")
            .WithRoot("key2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subchild1", "subchdvalue2");
                    child.WithRoot("subchild2", "subchdvalue3");
                })
                .WithChildren("child2", child =>
                {
                    child.WithRoot("subchild3", "subchdvalue4");
                    child.WithRoot("subchild4", "subchdvalue5");
                })
            .Create();

            // JSON di destinazione
            var target = new CreateJsonScenario()
            .WithRoot("key1", "value1")
            .WithRoot("key2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subchild1", "subchdvalueTarget2");
                    child.WithRoot("subchild2", "subchdvalueTarget3");
                })
                .WithChildren("child2", child =>
                {
                    child.WithRoot("subchild3", "subchdvalueTarget4");
                })
            .Create();

            string expectedJson = new CreateJsonScenario()
            .WithRoot("key1", "value1")
            .WithRoot("key2")
                .WithChildren("child1", child =>
                {
                    child.WithRoot("subchild1", "subchdvalueTarget2");
                    child.WithRoot("subchild2", "subchdvalueTarget3");
                })
                .WithChildren("child2", child =>
                {
                    child.WithRoot("subchild3", "subchdvalueTarget4");
                    child.WithRoot("subchild4", "subchdvalue5");
                })
            .Create();

            var item = new JsonPorting.JsonPorting.JsonPorting();
            var mergedStr = item.CopyMissingKeysAndValues(source, target);

            var isEqual = JsonFactory.CompareTwoJson(expectedJson, mergedStr);
            isEqual.Should().BeTrue();
        }

    }
}