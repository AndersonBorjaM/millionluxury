﻿using MediatR;
using Million.Domain.Abstractions;

namespace Million.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {

    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {

    }

    public interface IBaseCommand
    { }
}
