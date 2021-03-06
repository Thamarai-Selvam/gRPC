# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc

import Image_pb2 as Image__pb2


class ImageServiceStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.GetImage = channel.unary_unary(
                '/Image.ImageService/GetImage',
                request_serializer=Image__pb2.ImageRequest.SerializeToString,
                response_deserializer=Image__pb2.ImageResponse.FromString,
                )
        self.GetImageList = channel.unary_unary(
                '/Image.ImageService/GetImageList',
                request_serializer=Image__pb2.ImageListRequest.SerializeToString,
                response_deserializer=Image__pb2.ImageListResponse.FromString,
                )


class ImageServiceServicer(object):
    """Missing associated documentation comment in .proto file."""

    def GetImage(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def GetImageList(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_ImageServiceServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'GetImage': grpc.unary_unary_rpc_method_handler(
                    servicer.GetImage,
                    request_deserializer=Image__pb2.ImageRequest.FromString,
                    response_serializer=Image__pb2.ImageResponse.SerializeToString,
            ),
            'GetImageList': grpc.unary_unary_rpc_method_handler(
                    servicer.GetImageList,
                    request_deserializer=Image__pb2.ImageListRequest.FromString,
                    response_serializer=Image__pb2.ImageListResponse.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'Image.ImageService', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class ImageService(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def GetImage(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/Image.ImageService/GetImage',
            Image__pb2.ImageRequest.SerializeToString,
            Image__pb2.ImageResponse.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def GetImageList(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/Image.ImageService/GetImageList',
            Image__pb2.ImageListRequest.SerializeToString,
            Image__pb2.ImageListResponse.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)
