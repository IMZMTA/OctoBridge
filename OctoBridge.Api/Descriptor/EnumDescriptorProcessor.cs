using NJsonSchema;
using System.Reflection;
using System.ComponentModel;
using NJsonSchema.Generation;

namespace OctoBridge.Api.Descriptor;

public class EnumDescriptionSchemaProcessor : ISchemaProcessor
{
    public void Process(SchemaProcessorContext context)
    {
        var type = context.ContextualType.Type;

        var enumType = Nullable.GetUnderlyingType(type) ?? type;

        if (!enumType.IsEnum)
            return;

        var enumNames = Enum.GetNames(enumType);
        var enumValues = Enum.GetValues(enumType);

        var descriptions = new List<string>();

        for (int i = 0; i < enumNames.Length; i++)
        {
            var member = enumType.GetMember(enumNames[i]).First();

            var descriptionAttr = member
                .GetCustomAttribute<DescriptionAttribute>();

            var description = descriptionAttr?.Description ?? enumNames[i];

            var value = Convert.ToInt32(enumValues.GetValue(i)!);

            descriptions.Add($"{value} = {description}");
        }

        if (string.IsNullOrEmpty(context.Schema.Description))
        {
            context.Schema.Description = string.Join("\n", descriptions);
        }
        else
        {
            context.Schema.Description += "\n\n" + string.Join("\n", descriptions);
        }
    }
}