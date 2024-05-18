// Made with Amplify Shader Editor v1.9.2.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Acronym/Sail_Shader"
{
	Properties
	{
		_Strength("Strength", Range( 0 , 1)) = 0.5
		_Scale("Scale", Float) = 0.7
		_NoiseScale("Noise Scale", Float) = 1
		_WindSpeed("Wind Speed", Float) = 1
		_Jitter("Jitter", Float) = 0.1
		_Mask("Mask", 2D) = "white" {}
		_Color1("Color 1", Color) = (0.08490568,0.08490568,0.08490568,0)
		_Color2("Color 2", Color) = (0.8207547,0.8207547,0.8207547,0)
		_Metallic("Metallic", Float) = 0
		_Smoothness("Smoothness", Float) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Scale;
		uniform float _Strength;
		uniform float _WindSpeed;
		uniform float _NoiseScale;
		uniform float _Jitter;
		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Metallic;
		uniform float _Smoothness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float2 CenteredUV15_g1 = ( v.texcoord.xy - float2( 0.5,0.5 ) );
			float2 break17_g1 = CenteredUV15_g1;
			float2 appendResult23_g1 = (float2(( length( CenteredUV15_g1 ) * 1.0 * 2.0 ) , ( atan2( break17_g1.x , break17_g1.y ) * ( 1.0 / 6.28318548202515 ) * 1.0 )));
			float temp_output_38_0 = ( 1.0 - pow( ( appendResult23_g1.x * _Scale ) , 1.0 ) );
			float2 temp_cast_0 = (( _Time.y * ( _Strength * _WindSpeed ) )).xx;
			float2 uv_TexCoord46 = v.texcoord.xy + temp_cast_0;
			float simplePerlin2D18 = snoise( uv_TexCoord46*_NoiseScale );
			simplePerlin2D18 = simplePerlin2D18*0.5 + 0.5;
			v.vertex.xyz += ( ase_vertexNormal * ( ( temp_output_38_0 + ( simplePerlin2D18 * _Jitter ) ) * _Strength ) );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 lerpResult28 = lerp( _Color1 , _Color2 , tex2D( _Mask, uv_Mask ));
			o.Albedo = lerpResult28.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19201
Node;AmplifyShaderEditor.SamplerNode;26;555.8523,-25.35815;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;28;905.8523,-174.3582;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-202.057,929.8295;Inherit;False;Property;_Jitter;Jitter;4;0;Create;True;0;0;0;False;0;False;0.1;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;27;313.8523,-74.35815;Inherit;True;Property;_Mask;Mask;5;0;Create;True;0;0;0;False;0;False;6bad79e6d177d3046a1b65c5de792014;c1ddd6607626daf498159233c7c9e0c2;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;32;1142.924,-74.19434;Inherit;False;Property;_Metallic;Metallic;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;1144.924,7.805664;Inherit;False;Property;_Smoothness;Smoothness;9;0;Create;True;0;0;0;False;0;False;0.5;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-932.25,872.6463;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-517.5499,889.5465;Inherit;False;Property;_NoiseScale;Noise Scale;2;0;Create;True;0;0;0;False;0;False;1;4.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1139.233,916.0092;Inherit;False;Property;_WindSpeed;Wind Speed;3;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;47;1406.557,-174.8899;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Acronym/Sail_Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;918.6652,181.1552;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalVertexDataNode;33;688.356,330.7571;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-1218.706,188.7661;Inherit;False;Property;_Strength;Strength;0;0;Create;True;0;0;0;False;0;False;0.5;0.141;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;10;-551.73,355.4987;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-325.73,354.4987;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;13;-902.7301,616.4985;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;18;-27.73011,632.4985;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-698.73,677.4985;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;257.2455,687.2187;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;37;-89.44601,334.1772;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;6;-865.7302,335.4987;Inherit;False;Polar Coordinates;-1;;1;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;38;85.55403,336.1772;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-393.4471,642.5356;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-516.73,489.4985;Inherit;False;Property;_Scale;Scale;1;0;Create;True;0;0;0;False;0;False;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;427.0989,365.4874;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;547.2468,183.7582;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;725.4157,693.2228;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;715.322,581.1898;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;50;949.0772,581.703;Inherit;True;Normal From Height;-1;;3;1942fe2c5f1a1f94881a33d532e4afeb;0;2;20;FLOAT;0;False;110;FLOAT;0.5;False;2;FLOAT3;40;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;30;615.8523,-383.3582;Inherit;False;Property;_Color1;Color 1;6;0;Create;True;0;0;0;False;0;False;0.08490568,0.08490568,0.08490568,0;0.1981132,0.1981132,0.1981132,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;29;619.8523,-208.3582;Inherit;False;Property;_Color2;Color 2;7;0;Create;True;0;0;0;False;0;False;0.8207547,0.8207547,0.8207547,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;26;0;27;0
WireConnection;28;0;30;0
WireConnection;28;1;29;0
WireConnection;28;2;26;0
WireConnection;44;0;23;0
WireConnection;44;1;15;0
WireConnection;47;0;28;0
WireConnection;47;3;32;0
WireConnection;47;4;31;0
WireConnection;47;11;43;0
WireConnection;43;0;33;0
WireConnection;43;1;22;0
WireConnection;10;0;6;0
WireConnection;11;0;10;0
WireConnection;11;1;12;0
WireConnection;18;0;46;0
WireConnection;18;1;45;0
WireConnection;14;0;13;0
WireConnection;14;1;44;0
WireConnection;20;0;18;0
WireConnection;20;1;21;0
WireConnection;37;0;11;0
WireConnection;38;0;37;0
WireConnection;46;1;14;0
WireConnection;19;0;38;0
WireConnection;19;1;20;0
WireConnection;22;0;19;0
WireConnection;22;1;23;0
WireConnection;49;0;38;0
WireConnection;49;1;23;0
WireConnection;50;20;49;0
WireConnection;50;110;48;0
ASEEND*/
//CHKSM=635B040183760A082260835E3FB9E48C48959E71