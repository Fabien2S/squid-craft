using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SquidCraft.Collections;
using SquidCraft.Utils;

namespace SquidCraft.Text.Serializers
{
    public class TextComponentSerializer : JsonConverter<TextComponent>
    {
        public override TextComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var text = reader.GetString();
                return new TextComponentString(text);
            }

            var document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            var component =  BuildComponent(root);
            return PopulateComponent(component, root);
        }

        private static TextComponent BuildComponent(JsonElement root)
        {
            if (root.TryGetProperty("text", out var prop))
            {
                var text = prop.GetString();
                return new TextComponentString(text);
            }
            
            if (root.TryGetProperty("translate", out prop))
            {
                var text = prop.GetString();
                var arguments = EmptyArray<string>.Instance;
                if (!root.TryGetProperty("with", out var args))
                    return new TextComponentTranslate(text, arguments);
                
                var arrayLength = args.GetArrayLength();
                var arrayEnumerator = args.EnumerateArray();
                
                var i = 0;
                arguments = new string[arrayLength];
                foreach (var element in arrayEnumerator)
                    arguments[i++] = element.GetString();

                return new TextComponentTranslate(text, arguments);
            }
            
            if (root.TryGetProperty("keybind", out prop))
            {
                var text = prop.GetString();
                return new TextComponentString(text);
            }

            var rawText = root.GetRawText();
            throw new JsonException("Invalid text component: " + rawText);
        }

        private static TextComponent PopulateComponent(TextComponent component, JsonElement root)
        {
            if(root.TryGetProperty("color", out var colorElement))
            {
                var color = colorElement.GetString();
                component.Color = TextComponentHelper.GetColorFromName(color);
            }

            var styles = TextComponentHelper.Styles;
            foreach (var style in styles)
            {
                var styleName = style.GetStyleName();
                if(!root.TryGetProperty(styleName, out var styleElement))
                    continue;
                
                var hasStyle = styleElement.GetBoolean();
                if (hasStyle)
                    component.Style |= style;
                else
                    component.Style &= ~style;
            }

            if (!root.TryGetProperty("extra", out var extraElement))
                return component;
            
            var enumerator = extraElement.EnumerateArray();
            foreach (var element in enumerator)
            {
                var child = BuildComponent(element);
                var populatedChild = PopulateComponent(child, element);
                component.Children.Add(populatedChild);
            }

            return component;
        }
        
        public override void Write(Utf8JsonWriter writer, TextComponent value, JsonSerializerOptions options)
        {
            WriteComponent(writer, value);
        }

        private static void WriteComponent(
            Utf8JsonWriter writer,
            TextComponent component,
            TextColor currentColor = TextColor.White,
            TextStyles currentStyle = TextStyles.None
        )
        {
            writer.WriteStartObject();
            
            switch (component)
            {
                case TextComponentString _:
                    writer.WriteString("text", component.Value);
                    break;
                case TextComponentTranslate componentTranslate:
                {
                    writer.WriteString("translate", component.Value);
                    
                    var parameters = componentTranslate.Parameters;
                    if (parameters.Length > 0)
                    {
                        writer.WritePropertyName("with");
                        writer.WriteStartArray();
                        foreach (var parameter in parameters)
                            writer.WriteStringValue(parameter);
                        writer.WriteEndArray();
                    }

                    break;
                }
                case TextComponentKeybind _:
                    writer.WriteString("keybind", component.Value);
                    break;
            }
            
            if (component.HasColor)
            {
                var color = component.Color;
                if (color != currentColor)
                {
                    var colorName = color.GetColorName();
                    writer.WriteString("color", colorName);
                }
            }

            if (component.HasStyle)
            {
                var styles = TextComponentHelper.Styles;
                foreach (var style in styles)
                {
                    var hasStyle = component.Style.HasFlag(style);
                    var parentHasStyle = currentStyle.HasFlag(style);
                    if (hasStyle == parentHasStyle)
                        continue;

                    var styleName = style.GetStyleName();
                    writer.WriteBoolean(styleName, hasStyle);
                }
            }

            var children = component.Children;
            if (children.Count > 0)
            {
                writer.WritePropertyName("extra");
                writer.WriteStartArray();
                foreach (var child in children)
                    WriteComponent(writer, child, component.Color, component.Style);
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}