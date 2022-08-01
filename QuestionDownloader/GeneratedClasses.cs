using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
	[XmlRoot(ElementName = "correctResponse", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class CorrectResponse
	{
		[XmlElement(ElementName = "value", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "responseDeclaration", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class ResponseDeclaration
	{
		[XmlElement(ElementName = "correctResponse", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public CorrectResponse CorrectResponse { get; set; }
		[XmlAttribute(AttributeName = "identifier")]
		public string Identifier { get; set; }
		[XmlAttribute(AttributeName = "cardinality")]
		public string Cardinality { get; set; }
		[XmlAttribute(AttributeName = "baseType")]
		public string BaseType { get; set; }
	}

	[XmlRoot(ElementName = "outcomeDeclaration", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class OutcomeDeclaration
	{
		[XmlAttribute(AttributeName = "identifier")]
		public string Identifier { get; set; }
		[XmlAttribute(AttributeName = "cardinality")]
		public string Cardinality { get; set; }
		[XmlAttribute(AttributeName = "baseType")]
		public string BaseType { get; set; }
	}

	[XmlRoot(ElementName = "simpleChoice", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class SimpleChoice
	{
		[XmlAttribute(AttributeName = "identifier")]
		public string Identifier { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "choiceInteraction", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class ChoiceInteraction
	{
		[XmlElement(ElementName = "simpleChoice", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public List<SimpleChoice> SimpleChoice { get; set; }
		[XmlAttribute(AttributeName = "responseIdentifier")]
		public string ResponseIdentifier { get; set; }
		[XmlAttribute(AttributeName = "shuffle")]
		public string Shuffle { get; set; }
		[XmlAttribute(AttributeName = "maxChoices")]
		public string MaxChoices { get; set; }
	}

	[XmlRoot(ElementName = "infoControl", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class InfoControl
	{
		[XmlAttribute(AttributeName = "label")]
		public string Label { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "itemBody", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class ItemBody
	{
		[XmlElement(ElementName = "choiceInteraction", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public ChoiceInteraction ChoiceInteraction { get; set; }
		[XmlElement(ElementName = "infoControl", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public InfoControl InfoControl { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "responseProcessing", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class ResponseProcessing
	{
		[XmlAttribute(AttributeName = "template")]
		public string Template { get; set; }
	}

	[XmlRoot(ElementName = "assessmentItem", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
	public class AssessmentItem
	{
		[XmlElement(ElementName = "responseDeclaration", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public ResponseDeclaration ResponseDeclaration { get; set; }
		[XmlElement(ElementName = "outcomeDeclaration", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public OutcomeDeclaration OutcomeDeclaration { get; set; }
		[XmlElement(ElementName = "itemBody", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public ItemBody ItemBody { get; set; }
		[XmlElement(ElementName = "responseProcessing", Namespace = "http://www.imsglobal.org/xsd/imsqti_v2p0")]
		public ResponseProcessing ResponseProcessing { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName = "identifier")]
		public string Identifier { get; set; }
		[XmlAttribute(AttributeName = "title")]
		public string Title { get; set; }
		[XmlAttribute(AttributeName = "adaptive")]
		public string Adaptive { get; set; }
		[XmlAttribute(AttributeName = "timeDependent")]
		public string TimeDependent { get; set; }
	}

}
