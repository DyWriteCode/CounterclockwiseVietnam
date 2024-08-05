// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ibase.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace Proto {

  #region Messages
  public sealed class Vector3 : pb::IMessage {
    private static readonly pb::MessageParser<Vector3> _parser = new pb::MessageParser<Vector3>(() => new Vector3());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Vector3> Parser { get { return _parser; } }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 1;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 2;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    /// <summary>Field number for the "z" field.</summary>
    public const int ZFieldNumber = 3;
    private float z_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Z {
      get { return z_; }
      set {
        z_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (X != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Z);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (Z != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 13: {
            X = input.ReadFloat();
            break;
          }
          case 21: {
            Y = input.ReadFloat();
            break;
          }
          case 29: {
            Z = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  public sealed class Rpc : pb::IMessage {
    private static readonly pb::MessageParser<Rpc> _parser = new pb::MessageParser<Rpc>(() => new Rpc());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Rpc> Parser { get { return _parser; } }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "target" field.</summary>
    public const int TargetFieldNumber = 3;
    private int target_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Target {
      get { return target_; }
      set {
        target_ = value;
      }
    }

    /// <summary>Field number for the "args" field.</summary>
    public const int ArgsFieldNumber = 4;
    private pb::ByteString args_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Args {
      get { return args_; }
      set {
        args_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Target != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Target);
      }
      if (Args.Length != 0) {
        output.WriteRawTag(34);
        output.WriteBytes(Args);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Target != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Target);
      }
      if (Args.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Args);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            Target = input.ReadInt32();
            break;
          }
          case 34: {
            Args = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  public sealed class InstantiateRequest : pb::IMessage {
    private static readonly pb::MessageParser<InstantiateRequest> _parser = new pb::MessageParser<InstantiateRequest>(() => new InstantiateRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<InstantiateRequest> Parser { get { return _parser; } }

    /// <summary>Field number for the "prefabName" field.</summary>
    public const int PrefabNameFieldNumber = 1;
    private string prefabName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PrefabName {
      get { return prefabName_; }
      set {
        prefabName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "position" field.</summary>
    public const int PositionFieldNumber = 2;
    private global::Proto.Vector3 position_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Proto.Vector3 Position {
      get { return position_; }
      set {
        position_ = value;
      }
    }

    /// <summary>Field number for the "direction" field.</summary>
    public const int DirectionFieldNumber = 3;
    private global::Proto.Vector3 direction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Proto.Vector3 Direction {
      get { return direction_; }
      set {
        direction_ = value;
      }
    }

    /// <summary>Field number for the "group" field.</summary>
    public const int GroupFieldNumber = 4;
    private int group_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Group {
      get { return group_; }
      set {
        group_ = value;
      }
    }

    /// <summary>Field number for the "args" field.</summary>
    public const int ArgsFieldNumber = 5;
    private pb::ByteString args_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pb::ByteString Args {
      get { return args_; }
      set {
        args_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (PrefabName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(PrefabName);
      }
      if (position_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Position);
      }
      if (direction_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Direction);
      }
      if (Group != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Group);
      }
      if (Args.Length != 0) {
        output.WriteRawTag(42);
        output.WriteBytes(Args);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (PrefabName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PrefabName);
      }
      if (position_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Position);
      }
      if (direction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Direction);
      }
      if (Group != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Group);
      }
      if (Args.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Args);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            PrefabName = input.ReadString();
            break;
          }
          case 18: {
            if (position_ == null) {
              position_ = new global::Proto.Vector3();
            }
            input.ReadMessage(position_);
            break;
          }
          case 26: {
            if (direction_ == null) {
              direction_ = new global::Proto.Vector3();
            }
            input.ReadMessage(direction_);
            break;
          }
          case 32: {
            Group = input.ReadInt32();
            break;
          }
          case 42: {
            Args = input.ReadBytes();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
