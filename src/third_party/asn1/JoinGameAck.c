/*
 * Generated by asn1c-0.9.22 (http://lionet.info/asn1c)
 * From ASN.1 module "POKERTH-PROTOCOL"
 * 	found in "../../../docs/pokerth.asn1"
 * 	`asn1c -fnative-types`
 */

#include <asn_internal.h>

#include "JoinGameAck.h"

static asn_TYPE_member_t asn_MBR_JoinGameAck_1[] = {
	{ ATF_NOFLAGS, 0, offsetof(struct JoinGameAck, areYouGameAdmin),
		(ASN_TAG_CLASS_UNIVERSAL | (1 << 2)),
		0,
		&asn_DEF_BOOLEAN,
		0,	/* Defer constraints checking to the member type */
		0,	/* PER is not compiled, use -gen-PER */
		0,
		"areYouGameAdmin"
		},
	{ ATF_NOFLAGS, 0, offsetof(struct JoinGameAck, gameInfo),
		(ASN_TAG_CLASS_UNIVERSAL | (16 << 2)),
		0,
		&asn_DEF_NetGameInfo,
		0,	/* Defer constraints checking to the member type */
		0,	/* PER is not compiled, use -gen-PER */
		0,
		"gameInfo"
		},
};
static ber_tlv_tag_t asn_DEF_JoinGameAck_tags_1[] = {
	(ASN_TAG_CLASS_UNIVERSAL | (16 << 2))
};
static asn_TYPE_tag2member_t asn_MAP_JoinGameAck_tag2el_1[] = {
    { (ASN_TAG_CLASS_UNIVERSAL | (1 << 2)), 0, 0, 0 }, /* areYouGameAdmin at 299 */
    { (ASN_TAG_CLASS_UNIVERSAL | (16 << 2)), 1, 0, 0 } /* gameInfo at 301 */
};
static asn_SEQUENCE_specifics_t asn_SPC_JoinGameAck_specs_1 = {
	sizeof(struct JoinGameAck),
	offsetof(struct JoinGameAck, _asn_ctx),
	asn_MAP_JoinGameAck_tag2el_1,
	2,	/* Count of tags in the map */
	0, 0, 0,	/* Optional elements (not needed) */
	1,	/* Start extensions */
	3	/* Stop extensions */
};
asn_TYPE_descriptor_t asn_DEF_JoinGameAck = {
	"JoinGameAck",
	"JoinGameAck",
	SEQUENCE_free,
	SEQUENCE_print,
	SEQUENCE_constraint,
	SEQUENCE_decode_ber,
	SEQUENCE_encode_der,
	SEQUENCE_decode_xer,
	SEQUENCE_encode_xer,
	0, 0,	/* No PER support, use "-gen-PER" to enable */
	0,	/* Use generic outmost tag fetcher */
	asn_DEF_JoinGameAck_tags_1,
	sizeof(asn_DEF_JoinGameAck_tags_1)
		/sizeof(asn_DEF_JoinGameAck_tags_1[0]), /* 1 */
	asn_DEF_JoinGameAck_tags_1,	/* Same as above */
	sizeof(asn_DEF_JoinGameAck_tags_1)
		/sizeof(asn_DEF_JoinGameAck_tags_1[0]), /* 1 */
	0,	/* No PER visible constraints */
	asn_MBR_JoinGameAck_1,
	2,	/* Elements count */
	&asn_SPC_JoinGameAck_specs_1	/* Additional specs */
};

