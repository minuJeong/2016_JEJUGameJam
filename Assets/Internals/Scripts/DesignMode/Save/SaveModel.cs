using System;


[Serializable]
public class SaveModel
{
	public Guid ID = Guid.NewGuid ();

	public string SaveName = "NoName";

	public HeadData HeadData;

	public BodyData BodyData;

	public RLegData RLegData;

	public LLegData LLegData;

	public override string ToString ()
	{
		return string.Format ("[JointData] {0}, {1}", SaveName, ID);
	}
}

[Serializable]
public class JointData
{
	public byte[] TextureData;
}

[Serializable]
public class HeadData : JointData
{
	public HeadJointDef JointDef;
}

[Serializable]
public class BodyData : JointData
{
	public BodyJointDef JointDef;
}

[Serializable]
public class RLegData : JointData
{
	public RLegJointDef JointDef;
}

[Serializable]
public class LLegData : JointData
{
	public LLegJointDef JointDef;
}