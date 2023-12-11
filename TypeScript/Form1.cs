using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;
using System.DirectoryServices.ActiveDirectory;
using static JSONtoTypeScript.Form1.JsonData;
using System.Xml.Linq;

namespace JSONtoTypeScript
{
    public enum AssociationMultiplicity
    {
        ZeroToMany,
        OneToMany,
        OneToOne
    }
    public partial class Form1 : Form
    {
        private readonly StringBuilder sourceCodeBuilder;
        private string selectedJsonFilePath;
        private string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || !char.IsUpper(input[0]))
                return input;

            char[] chars = input.ToCharArray();
            chars[0] = char.ToLowerInvariant(chars[0]);
            return new string(chars);
        }

        private string ToLowerCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToLower(input[0]) + input.Substring(1);
        }

        public Form1()
        {
            InitializeComponent();
            sourceCodeBuilder = new StringBuilder();

        }

        private void GenerateTypeScriptCode(string jsonFilePath)
        {
            // Read JSON file content
            string umlDiagramJson = File.ReadAllText(jsonFilePath);
            // Decode JSON data
            JsonData json = JsonConvert.DeserializeObject<JsonData>(umlDiagramJson);



            foreach (var model in json.model)
            {
                if (model.type == "class")
                {
                    GenerateTypeScriptClass(model);
                }
                else if (model.type == "association" && model.model != null)
                {
                    GenerateTypeScriptAssosiationClass(model.model);
                }
            }
            // Display or save the generated TypeScript code
            richTextBox2.Text = sourceCodeBuilder.ToString();



        }

        private void GenerateTypeScriptClass(JsonData.Model model)
        {
            sourceCodeBuilder.AppendLine($"class {model.class_name} {{");

            foreach (var attribute in model.attributes)
            {
                GenerateTypeScriptAttribute(attribute);
            }

            sourceCodeBuilder.AppendLine("");

            foreach (var status in model.attributes)
            {
                GenerateTypeScriptState(status);
            }

            sourceCodeBuilder.AppendLine("");

            if (model.attributes != null)
            {
                GenerateTypeScriptConstructor(model.attributes);
            }

            sourceCodeBuilder.AppendLine("");

            foreach (var attribute in model.attributes)
            {
                GenerateTypeScriptGetterData(attribute);
            }

            sourceCodeBuilder.AppendLine("");

            foreach (var attribute in model.attributes)
            {
                GenerateTypeScriptSetterData(attribute);
            }

            sourceCodeBuilder.AppendLine("");

            if (model.states != null)
            {
                foreach (var state in model.states)
                {
                    GenerateTypeScriptStateTransition(state);
                }
            }

            if (model.states != null)
            {
                GenerateTypeScriptGetState();
            }


            sourceCodeBuilder.AppendLine("}\n\n");

        }


        private void GenerateTypeScriptAttribute(JsonData.Attribute1 attribute)
        {
            // Adjust data types as needed
            string dataType = MapDataType(attribute.data_type);

            if (dataType != "state")
            {
                sourceCodeBuilder.AppendLine($"    private  {attribute.attribute_name}: {dataType};");
            }
        }

        private void GenerateTypeScriptAssosiationClass(JsonData.Model associationModel)
        {
            // Check if associationModel is not null
            if (associationModel == null)
            {
                // Handle the case where associationModel is null, e.g., throw an exception or log a message
                return;
            }

            sourceCodeBuilder.AppendLine($"class assoc_{associationModel.class_name} {{");

            foreach (var attribute in associationModel.attributes)
            {
                // Adjust data types as needed
                string dataType = MapDataType(attribute.data_type);

                sourceCodeBuilder.AppendLine($"     private  {attribute.attribute_name}: {dataType};");
            }

            // Check if associatedClass.@class is not null before iterating
            if (associationModel.@class != null)
            {
                foreach (var associatedClass in associationModel.@class)
                {
                    if (associatedClass.class_multiplicity == "1..1")
                    {
                        sourceCodeBuilder.AppendLine($"    private {associatedClass.class_name} ${associatedClass.class_name};");
                    }
                    else
                    {
                        sourceCodeBuilder.AppendLine($"    private array ${associatedClass.class_name}List;");
                    }
                }
            }

            sourceCodeBuilder.AppendLine("");

            if (associationModel.attributes != null)
            {
                GenerateTypeScriptConstructor(associationModel.attributes);
            }

            foreach (var attribute in associationModel.attributes)
            {
                GenerateTypeScriptGetterData(attribute);
            }

            foreach (var attribute in associationModel.attributes)
            {
                GenerateTypeScriptSetterData(attribute);
            }
            sourceCodeBuilder.AppendLine("}\n\n");
        }



        //Constructor
        private void GenerateTypeScriptConstructor(List<JsonData.Attribute1> attributes)
        {
            sourceCodeBuilder.Append($"     constructor(");

            foreach (var attribute in attributes)
            {
                if (attribute.attribute_name != "status")
                {
                    string dataType = MapDataType(attribute.data_type);
                    sourceCodeBuilder.Append($"{attribute.attribute_name}: {dataType},");
                }

            }

            // Remove the trailing comma and add the closing parenthesis
            if (attributes.Any())
            {
                sourceCodeBuilder.Length -= 1; // Remove the last character (",")
            }

            sourceCodeBuilder.AppendLine(") {");

            foreach (var attribute in attributes)
            {
                if (attribute.attribute_name != "status")
                {
                    sourceCodeBuilder.AppendLine($"        this.{attribute.attribute_name} = {attribute.attribute_name};");
                }
            }

            sourceCodeBuilder.AppendLine("}");
        }


        //Getter
        private void GenerateTypeScriptGetterData(JsonData.Attribute1 getter)
        {
            if (getter.attribute_name != "status")
            {
                string dataType = MapDataType(getter.data_type);
                sourceCodeBuilder.AppendLine($"      public  get{getter.attribute_name}() : {dataType} {{");
                sourceCodeBuilder.AppendLine($"        return this.{getter.attribute_name};");
                sourceCodeBuilder.AppendLine($"}}");
            }
        }


        //Setter
        private void GenerateTypeScriptSetterData(JsonData.Attribute1 setter)
        {
            if (setter.attribute_name != "status")
            {
                string dataType = MapDataType(setter.data_type);
                sourceCodeBuilder.AppendLine($"      public  set{setter.attribute_name}({setter.attribute_name} : {dataType}) : void {{");
                sourceCodeBuilder.AppendLine($"         this.{setter.attribute_name} = {setter.attribute_name};");
                sourceCodeBuilder.AppendLine($"}}");
            }
        }

        private void GenerateTypeScriptState(JsonData.Attribute1 status)
        {
            if (status.attribute_name == "status")
            {
                sourceCodeBuilder.AppendLine("    private state;");
            }
        }

        private void GenerateTypeScriptGetState()
        {
            
            sourceCodeBuilder.AppendLine($"     public  GetState(): string {{");
            sourceCodeBuilder.AppendLine($"      return  this.state;");
            sourceCodeBuilder.AppendLine($"}}\n");
        }

        private void GenerateTypeScriptStateTransition(JsonData.State state)
        {
            if (state.state_event != null && state.state_event.Length > 0)
            {
                string setEvent = state.state_event[0];
                string onEvent = state.state_event[1];
                sourceCodeBuilder.AppendLine($"     public  {setEvent}() {{");
                sourceCodeBuilder.AppendLine($"       this.state = \"{state.state_value}\";");
                sourceCodeBuilder.AppendLine($"}}\n");

                sourceCodeBuilder.AppendLine($"     public  {onEvent}() {{");
                sourceCodeBuilder.AppendLine($"      console.log( \"status saat ini {state.state_value}\");");
                sourceCodeBuilder.AppendLine($"}}");
            }


            if (state.state_function != null && state.state_function.Length > 0)
            {
                string setFunction = state.state_function[0];
                sourceCodeBuilder.AppendLine($"     public  {setFunction}() {{");
                sourceCodeBuilder.AppendLine($"       this.state = \"{state.state_value}\";");
                sourceCodeBuilder.AppendLine($"}}\n");
            }
        }



   



        private string GetDefaultValueCode(object defaultValue)
        {
            if (defaultValue == null)
            {
                return "null";
            }

            if (defaultValue is string)
            {
                return $"\"{defaultValue}\"";
            }

            return defaultValue.ToString();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Json Diagram File";
            dialog.Filter = "Json Diagram Files|*.json";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedJsonFilePath = dialog.FileName;
                string displayJson = File.ReadAllText(selectedJsonFilePath);
                tabControl1.SelectTab(tabPage1);
                richTextBox1.Text = displayJson;

                // Set the path to textBox1
                textBox1.Text = selectedJsonFilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(selectedJsonFilePath) && File.Exists(selectedJsonFilePath))
                {
                    GenerateTypeScriptCode(selectedJsonFilePath);
                }
                else
                {
                    MessageBox.Show("Please select a valid JSON file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating TypeScript code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }



         public class JsonData
        {
            public string type { get; set; }
            public string sub_id { get; set; }
            public string sub_name { get; set; }
            public List<Model> model { get; set; }
            public class Model
            {
                public string type { get; set; }
                public string class_id { get; set; }
                public string class_name { get; set; }
                public string KL { get; set; }
                public List<Attribute1> attributes { get; set; }
                public List<State> states { get; set; }
                public Model model { get; set; }
                public List<Class1> @class { get; set; }
            }

            public class Attribute1
            {
                public string attribute_name { get; set; }
                public string data_type { get; set; }
                public string default_value { get; set; }
                public string attribute_type { get; set; }
            }

            public class State
            {
                public string state_id { get; set; }
                public string state_name { get; set; }
                public string state_value { get; set; }
                public string state_type { get; set; }
                public string[] state_event { get; set; }
                public string[] state_function { get; set; }
            }

            public class Class1
            {
                public string class_name { get; set; }
                public string class_multiplicity { get; set; }
                public List<Attribute> attributes { get; set; }
                public List<Class1> @class { get; set; }
            }

            public class Attribute
            {
                public string attribute_name { get; set; }
                public string data_type { get; set; }
                public string attribute_type { get; set; }
            }
        }

        private string MapDataType(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "integer":
                    return "number";
                case "id":
                    return "number";
                case "string":
                    return "string";
                case "bool":
                    return "bool";
                case "real":
                    return "number";
                // Add more mappings as needed
                default:
                    return dataType; // For unknown types, just pass through
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TypeScript Files|*.ts";
            saveFileDialog.Title = "Save TypeScript File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        
                        sw.Write(richTextBox2.Text);
                    }

                    MessageBox.Show("File berhasil disimpan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}